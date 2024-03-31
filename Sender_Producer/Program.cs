using RabbitMQ.Client;
using SayyehBanTools.ConnectionDB;
using System.Text;

var factory = new ConnectionFactory();
Uri connectionUri = RabbitMQConnection.DefaultConnection();
factory.Uri = new Uri(connectionUri.OriginalString);
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string QueueName = "zljn7vcbl3t28usw";
/*
## توضیح دستور `channel.QueueDeclare(QueueName, true, false, false, null)` در RabbitMQ

دستور `channel.QueueDeclare` برای ایجاد یا بررسی وجود یک صف در RabbitMQ استفاده می شود. پارامترهای این دستور به شرح زیر هستند:

* **QueueName:** نام صف.
* **durable:** اگر `true` باشد، صف پس از قطع شدن اتصال به سرور RabbitMQ باقی می ماند. در غیر این صورت، صف پس از قطع شدن اتصال حذف می شود.
* **exclusive:** اگر `true` باشد، صف فقط توسط یک مصرف کننده قابل استفاده است. در غیر این صورت، صف می تواند توسط چندین مصرف کننده استفاده شود.
* **autoDelete:** اگر `true` باشد، صف به طور خودکار حذف می شود زمانی که هیچ مصرف کننده ای به آن متصل نباشد. در غیر این صورت، صف باید به طور دستی حذف شود.
* **arguments:** آرگومان های اضافی برای پیکربندی صف.

**در مورد پارامتر `durable`:**

* اگر `true` باشد:
    * صف پس از قطع شدن اتصال به سرور RabbitMQ باقی می ماند.
    * پیام های ارسال شده به صف حتی پس از قطع شدن اتصال به سرور RabbitMQ حفظ می شوند.
    * مصرف کنندگان می توانند پس از اتصال مجدد به سرور RabbitMQ، پیام ها را از صف دریافت کنند.
* اگر `false` باشد:
    * صف پس از قطع شدن اتصال به سرور RabbitMQ حذف می شود.
    * پیام های ارسال شده به صف پس از قطع شدن اتصال به سرور RabbitMQ از بین می روند.
    * مصرف کنندگان نمی توانند پس از اتصال مجدد به سرور RabbitMQ، پیام ها را از صف دریافت کنند.

**انتخاب بین `true` و `false` برای پارامتر `durable` به نیازهای شما بستگی دارد:**

* اگر می خواهید پیام ها حتی پس از قطع شدن اتصال به سرور RabbitMQ حفظ شوند، باید `durable` را `true` تنظیم کنید.
* اگر می خواهید صف به طور خودکار پس از قطع شدن اتصال به سرور RabbitMQ حذف شود، می توانید `durable` را `false` تنظیم کنید.

 */
channel.QueueDeclare(QueueName, true, false, false, null);
for (int i = 0; i < 1000000; i++)
{
    string message = $"This is a test meassage from my sender at :{DateTime.Now.Ticks}";
    var body = Encoding.UTF8.GetBytes(message);
    var properties=channel.CreateBasicProperties();
    /*اگر این مقدار برابر با 
     * true
     * باشه باعث میشه مقدار اگر 
     * RabbitMQ
     * خاموش شد باعث تو دیسک ذخیره بشه
     * */
    properties.Persistent = true;
    channel.BasicPublish("", QueueName, properties, body);
}



channel.Close();
connection.Close();
Console.ReadLine();