using Amazon;
using System;
using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AmazonSESSample
{
    class Program
    {
        static readonly string senderAddress = "emailtestinn123@gmail.com";

        static readonly string receiverAddress = "testemailinn@gmail.com";

        // The configuration set to use for this email. If you do not want to use a
        // configuration set, comment out the following property and the
        // ConfigurationSetName = configSet argument below. 
        //static readonly string configSet = "ConfigSet";

        static readonly string textBody = "Amazon SES Test (.NET)\r\n"
                                        + "This email was sent through Amazon SES "
                                        + "using the AWS SDK for .NET.";

        static readonly string htmlBody = @"<html>
<head></head>
<body>
  <h1>Amazon SES Test (AWS SDK for .NET)</h1>
  <p>This email was sent with
    <a href='https://aws.amazon.com/ses/'>Amazon SES</a> using the
    <a href='https://aws.amazon.com/sdk-for-net/'>
      AWS SDK for .NET</a>.</p>
</body>
</html>";

        static void Main(string[] args)
        {
            
            var watch = new Stopwatch();
            watch.Start();
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USWest2))
            {
                for (int i = 1; i <= 30; i++)
                {
                    var subject = $"Amazon SES test {i} (AWS SDK for .NET)";
                    var sendRequest = new SendEmailRequest
                    {
                        Source = senderAddress,
                        Destination = new Destination
                        {
                            ToAddresses =
                            new List<string> { receiverAddress }
                        },

                        Message = new Message
                        {
                            Subject = new Content(subject),
                            Body = new Body
                            {
                                Html = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = htmlBody
                                },
                                Text = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = textBody
                                }
                            }
                        },
                        // If you are not using a configuration set, comment
                        // or remove the following line 
                        //ConfigurationSetName = configSet
                    };
                    try
                    {
                        Console.WriteLine("Sending email using Amazon SES...");
                        Parallel.Invoke(
                            () =>
                            {
                                #region Starting
                                Debug.WriteLine("Starting Operation 1");
                                #endregion
                                var response = client.SendEmail(sendRequest);
                                #region Ending
                                Debug.WriteLine("Completed Operation 1");
                                #endregion
                            },
                            () =>
                            {
                                #region Starting
                                Debug.WriteLine("Starting Operation 2");
                                #endregion
                                var response = client.SendEmail(sendRequest);
                                #region Ending
                                Debug.WriteLine("Completed Operation 2");
                                #endregion
                            },
                            () =>
                            {
                                #region Starting
                                Debug.WriteLine("Starting Operation 3");
                                #endregion
                                var response = client.SendEmail(sendRequest);
                                #region Ending
                                Debug.WriteLine("Completed Operation 3");
                                #endregion
                            },
                            () =>
                            {
                                #region Starting
                                Debug.WriteLine("Starting Operation 4");
                                #endregion
                                var response = client.SendEmail(sendRequest);
                                #region Ending
                                Debug.WriteLine("Completed Operation 4");
                                #endregion
                            }
                            );
                        Console.WriteLine("The email was sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The email was not sent.");
                        Console.WriteLine("Error message: " + ex.Message);

                    }
                }
                watch.Stop();
                TimeSpan ts = watch.Elapsed;

                Console.WriteLine($"It took {ts.Seconds} seconds to send 30 emails");

                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
