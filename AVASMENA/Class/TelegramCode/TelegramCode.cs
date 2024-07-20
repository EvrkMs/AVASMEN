using jsonData;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramCode
{
    public static class Telegrame
    {
        private static readonly Dictionary<string, int> userOffsets = new Dictionary<string, int>();
        private static readonly Dictionary<string, long> users = UserDataLoader.LoadFromFile().Users;
        private static readonly Dictionary<string, int> names = UserDataLoader.LoadFromFile().Names;
        private static readonly string token = UserDataLoader.LoadFromFile().TokenBot;

        private static bool isSending = false;
        private static readonly ITelegramBotClient bot = new TelegramBotClient(token);
        private static readonly long forwardChatId = UserDataLoader.LoadFromFile().ForwardChat;
        private static readonly long chatID = UserDataLoader.LoadFromFile().ChatId;

        public static async Task ProcessUpdates(long userId, int TredID, string selectedName, ListBox listBox, MaterialButton button)
        {
            int offset = 0;
            DateTime requestTimestamp = DateTime.UtcNow;
            var photosToBeSent = new List<IAlbumInputMedia>();

            // Проверка, если уже сохранен offset для текущего пользователя, используйте его
            if (userOffsets.ContainsKey(selectedName))
            {
                offset = userOffsets[selectedName];
            }

            if (isSending)
            {
                listBox.Items.Clear();
                listBox.Items.Add("Отчет в процессе отправки. Пожалуйста, дождитесь окончания работы таймера.");
                return;
            }

            button.Enabled = false;
            listBox.Items.Clear();
            listBox.Items.Add($"Запрос отправлен в {DateTime.Now:T}");
            listBox.Items.Add($"Ожидание фотографий. Пожалуйста, отправьте фотографии...");

            if (string.IsNullOrWhiteSpace(selectedName))
            {
                MessageBox.Show("Вы забыли выбрать имя из выпадающего списка, NoName");
                isSending = false;
                button.Enabled = true;
                return;
            }

            if (!users.ContainsKey(selectedName))
            {
                MessageBox.Show("Выбранный пользователь не существует.");
                isSending = false;
                button.Enabled = true;
                return;
            }

            var message = await bot.SendTextMessageAsync(userId, "Ожидаю ваши фотографии...");

            Console.WriteLine("TaskRun");
            while (true)
            {
                // Отправка запроса на получение обновлений с использованием текущего offset
                var updates = await bot.GetUpdatesAsync(offset: offset, timeout: 1);
                photosToBeSent.Clear(); // Очищаем список перед началом сбора фотографий для нового пользователя
                Console.WriteLine("While");
                foreach (var update in updates)
                {
                    Console.WriteLine("update");
                    if (update.Message != null && update.Message.Photo != null && update.Message.Chat.Id == userId)
                    {
                        Console.WriteLine("2");
                        // Проверяем время отправки фотографии
                        if (update.Message.Date > requestTimestamp)
                        {
                            string fileId = update.Message.Photo.Last().FileId;
                            photosToBeSent.Add(new InputMediaPhoto(fileId));
                            Console.WriteLine("3");
                        }
                    }
                    // Обновление offset для текущего пользователя
                    if (updates.Any())
                    {
                        offset = updates.Max(u => u.Id) + 1;
                        userOffsets[selectedName] = offset;
                    }

                }

                if (photosToBeSent.Count > 0)
                {
                    listBox.Invoke((MethodInvoker)delegate
                    {
                        listBox.Items.Add($"Получено {photosToBeSent.Count} фотографий. Отправляю в чат...");
                    });

                    var mediaItems = photosToBeSent.Select(photo => (IAlbumInputMedia)photo).ToList();
                    await bot.SendMediaGroupAsync(forwardChatId, mediaItems, replyToMessageId: TredID);

                    listBox.Invoke((MethodInvoker)delegate
                    {
                        listBox.Items.Add($"Отправлено {photosToBeSent.Count} фотографий в чат.");
                    });
                    photosToBeSent.Clear();
                    break;
                }

                await Task.Delay(1000);
            }
            button.Enabled = true;
            photosToBeSent.Clear();
            isSending = false;
            return;
        }

        public static async Task SendMessageAsync(string zp1, string zp2, string name, string name2, string name3, MaterialButton button)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (names.ContainsKey(name))
                await bot.SendTextMessageAsync(chatID, zp1, replyToMessageId: names[name]);

            if (!string.IsNullOrWhiteSpace(name2) && names.ContainsKey(name2))
                await bot.SendTextMessageAsync(chatID, zp2, replyToMessageId: names[name2]);
            if (!string.IsNullOrWhiteSpace(name3) && names.ContainsKey(name3))
                await bot.SendTextMessageAsync(chatID, zp2, replyToMessageId: names[name3]);

            isSending = false;
            button.Enabled = true;
        }

        public static async void SendMessageToSelectedNames(List<string> selectedNames, string message)
        {
            foreach (var name in selectedNames)
            {
                if (names.TryGetValue(name, out int id))
                {
                    await bot.SendTextMessageAsync(chatID, message, replyToMessageId: id);
                }
            }
        }
    }
}