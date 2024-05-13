using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace TelegramCode
{
    public static class Telegrame
    {
        private static readonly Dictionary<string, int> userOffsets = new Dictionary<string, int>();
        private static readonly Dictionary<string, long> users = new Dictionary<string, long>
        {
            {"Ярый", 1972629490 },
            {"Серый", 986504267 },
            {"Вова", 5784613858},
            {"Егор",  917263855},
            {"Дима По", 1497063301 },
            {"Али",  5540567292},
            {"Илья",  5107083008}
        };
        private static readonly Dictionary<string, int> names = new Dictionary<string, int>
        {
            { "Вова", 6 },
            { "Ярый", 178 },
            { "Серый", 448 },
            { "Егор", 12 },
            { "Дима По", 913 },
            { "Али", 11 },
            { "Илья", 10 }
        };

        private static bool isSending = false;
        private static readonly Telegram.Bot.TelegramBotClient bot = new Telegram.Bot.TelegramBotClient("6375453330:AAFIKOIwAztwY4__CF2c_vZvzcNuUf4l3KM");
        private static readonly long forwardChatId = -1002066018588;
        private static readonly long chatID = -1001990911245;

        public static async Task ProcessUpdates(long userId, int TredID, string selectedName, ListBox listBox, MaterialButton button)
        {

            int offset = 0;
            DateTime requestTimestamp = DateTime.UtcNow;
            var photosToBeSent = new List<IAlbumInputMedia>();
            DateTime waitForPhotosTimestamp = DateTime.MinValue;
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
            waitForPhotosTimestamp = DateTime.UtcNow;

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
                        string fileId = update.Message.Photo.Last().FileId;
                        photosToBeSent.Add(new InputMediaPhoto(new InputMedia(fileId)));
                        Console.WriteLine("3");
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

                    var mediaItems = photosToBeSent.Select(photo => new InputMediaPhoto(photo.Media.FileId)).ToList<IAlbumInputMedia>();
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
        public static async Task SendMessageAsync(string zp1, string zp2, string name, string name2, MaterialButton button)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            if (names.ContainsKey(name))
                await bot.SendTextMessageAsync(chatID, zp1, replyToMessageId: names[name]);

            if (!string.IsNullOrWhiteSpace(name2) && names.ContainsKey(name2))
                await bot.SendTextMessageAsync(chatID, zp2, replyToMessageId: names[name2]);

            isSending = false;
            button.Enabled = true;
        }
        public static async Task PhotoExcel(string screenshotPath)
        {
            // Отправляем скриншот в Telegram
            using (var stream = new MemoryStream(System.IO.File.ReadAllBytes(screenshotPath)))
            {
                await bot.SendPhotoAsync(forwardChatId, new InputOnlineFile(stream, "excel_table_screenshot.png"));
            }
        }

    }
}
