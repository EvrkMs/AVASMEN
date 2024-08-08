using APIData;
using Excel;
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
    public class Telegrame
    {
        private static readonly Dictionary<string, int> userOffsets = new Dictionary<string, int>();

        private static bool isSending = false;
        public Telegrame()
        {
            LoadData();
        }
        private void LoadData()
        {
            DataStore.Initialize();
        }
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

            if (!DataStore.Users.ContainsKey(selectedName))
            {
                MessageBox.Show("Выбранный пользователь не существует.");
                isSending = false;
                button.Enabled = true;
                return;
            }
            ITelegramBotClient bot = new TelegramBotClient(DataStore.TokenBot);
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
                    await bot.SendMediaGroupAsync(DataStore.ForwardChat, mediaItems, replyToMessageId: TredID);

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

        public static async Task SendMessageAsync(int zap1, int zap2, int zap3, string name, string name2, string name3, MaterialButton button)
        {
            ITelegramBotClient bot = new TelegramBotClient(DataStore.TokenBot);
            if (string.IsNullOrWhiteSpace(name))
                return;

            var zp1 = $"{DateTime.Now:yyyy.MM.dd}\n+{zap1}p";
            var zp2 = $"{DateTime.Now:yyyy.MM.dd}\n+{zap2}p";
            var zp3 = $"{DateTime.Now:yyyy.MM.dd}\n+{zap3}p";

            int value1 = ExcelHelper.GetCellValueAsInt("ZP", name);

            if (DataStore.Names.ContainsKey(name))
                await bot.SendTextMessageAsync(DataStore.ChatId, $"{zp1}\n теперь: {value1}", replyToMessageId: DataStore.Names[name]);

            if (!string.IsNullOrWhiteSpace(name2) && DataStore.Names.ContainsKey(name2))
            {
                int value2 = ExcelHelper.GetCellValueAsInt("ZP", name2);
                await bot.SendTextMessageAsync(DataStore.ChatId, $"{zp2}\n теперь: {value2}", replyToMessageId: DataStore.Names[name2]);
            }

            if (!string.IsNullOrWhiteSpace(name3) && DataStore.Names.ContainsKey(name3))
            {
                int value3 = ExcelHelper.GetCellValueAsInt("ZP", name3);
                await bot.SendTextMessageAsync(DataStore.ChatId, $"{zp3}\n теперь: {value3}", replyToMessageId: DataStore.Names[name3]);
            }

            isSending = false;
            button.Enabled = true;
        }

        public static async void SendMessageToSelectedNames(List<string> selectedNames, string message)
        {
            ITelegramBotClient bot = new TelegramBotClient(DataStore.TokenBot);
            foreach (var name in selectedNames)
            {
                if (DataStore.Names.TryGetValue(name, out int id))
                {
                    await bot.SendTextMessageAsync(DataStore.ChatId, message, replyToMessageId: id);
                }
            }
        }
    }
}