using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string QueueName = "zljn7vcbl3t28usw";
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Received Message " + message);
    Thread.Sleep(1);
    /*
     اگر این دستور
    channel.BasicConsume(QueueName, false, consumer);
    مقدار 
    false
    بگیره باید دستی پیام حذف کنم
    اما وقتی این دستور بنویسم
       channel.BasicAck(args.DeliveryTag,true);
    بهش میگم بعد از نمایش هر مقدار اون از تو صف پاک کن این طوری اگر ارتباط با 
    RabbitMq
    قطع بشه و برنامه دوباره باز بشه باز از ادامه اطلاعاتی که ارسال نشده باز ارسال ادامه میده

     */
    channel.BasicAck(args.DeliveryTag,true);
};
/*
 ## توضیح دستور `channel.BasicConsume(QueueName, false, consumer)` در RabbitMQ

دستور `channel.BasicConsume` برای شروع مصرف پیام ها از یک صف در RabbitMQ استفاده می شود. پارامترهای این دستور به شرح زیر هستند:

* **QueueName:** نام صف.
* **autoAck:** اگر `true` باشد، RabbitMQ به طور خودکار پیام ها را پس از دریافت توسط مصرف کننده تأیید می کند. در غیر این صورت، مصرف کننده باید به طور دستی پیام ها را تأیید کند.
* **consumer:**  یک رابط `IConsumer` که پیاده سازی نحوه پردازش پیام ها توسط مصرف کننده را ارائه می دهد.

**در مورد پارامتر `autoAck`:**

* اگر `true` باشد:
    * RabbitMQ به طور خودکار پیام ها را پس از دریافت توسط مصرف کننده تأیید می کند.
    * اگر مصرف کننده قبل از تأیید پیام ها از کار بیفتد، پیام ها دوباره به صف ارسال می شوند.
* اگر `false` باشد:
    * مصرف کننده باید به طور دستی پیام ها را تأیید کند.
    * اگر مصرف کننده قبل از تأیید پیام ها از کار بیفتد، پیام ها از بین می روند.

**انتخاب بین `true` و `false` برای پارامتر `autoAck` به نیازهای شما بستگی دارد:**

* اگر می خواهید اطمینان حاصل کنید که پیام ها فقط یک بار پردازش می شوند، باید `autoAck` را `false` تنظیم کنید.
* اگر می خواهید از پردازش مجدد پیام ها در صورت خرابی مصرف کننده جلوگیری کنید، می توانید `autoAck` را `true` تنظیم کنید.
 */
channel.BasicConsume(QueueName, false, consumer);

Console.ReadLine();