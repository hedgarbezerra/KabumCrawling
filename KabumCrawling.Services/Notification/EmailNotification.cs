using KabumCrawling.Domain.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Notification
{
    public class EmailNotification : NotificationBase
    {

        public override void Notificar(List<Produto> produtos, Destinario destinario)
        {
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Hedgar Bezerra", _email));
			message.To.Add(new MailboxAddress(destinario.Nome, destinario.Email));
			message.Subject = "Encontramos produtos do seu interesse aqui!";

			message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = MontaEmail(destinario, produtos)
			};

			using (var client = new SmtpClient())
			{
				client.Connect(_smtp, _smtpPort, true);

				client.Authenticate(_email, _emailPassword);

				client.Send(message);
				client.Disconnect(true);
			}
        }

        public override void Notificar(List<Destinario> destinario)
        {
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Hedgar Bezerra", _email));			
			message.Subject = "Encontramos produtos do seu interesse aqui!";

			destinario.ForEach(x => message.To.Add(new MailboxAddress(x.Nome, x.Email)));

			message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = @"Hey Chandler,
				I just wanted to let you know that Monica and I were going to go play some paintball, you in?
				-- Joey"
			};

			using (var client = new SmtpClient())
			{
				client.Connect(_smtp, _smtpPort, true);

				client.Authenticate(_email, _emailPassword);

				client.Send(message);
				client.Disconnect(true);
			}
		}
		private string MontaEmail(Destinario destinario, List<Produto> produtos)
        {
            string htmlEmail = $@"<!DOCTYPE html
                          PUBLIC '-//W3C//DTD XHTML 1.0 Transitional //EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                        <!--[if IE]><html xmlns='http://www.w3.org/1999/xhtml' class='ie'><![endif]-->
                        <!--[if !IE]><!-->
                        <html style='margin: 0;padding: 0;' xmlns='http://www.w3.org/1999/xhtml'>
                        <!--<![endif]-->

                        <head>
                          <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                          <title></title>
                          <!--[if !mso]><!-->
                          <meta http-equiv='X-UA-Compatible' content='IE=edge' />
                          <!--<![endif]-->
                          <meta name='viewport' content='width=device-width' />
  
                          <meta name='x-apple-disable-message-reformatting' />
                          <!--[if !mso]><!-->
                          <style type='text/css'>
                            @import url(https://fonts.googleapis.com/css?family=Crete+Round:400,400italic);
                          </style>
                          <link href='https://fonts.googleapis.com/css?family=Crete+Round:400,400italic' rel='stylesheet' type='text/css' />
                          <!--<![endif]-->
                          <meta name='robots' content='noindex,nofollow' />
                          <meta property='og:title' content='My First Campaign' />
                        </head>
                        <!--[if mso]>
                          <body class='mso'>
                        <![endif]-->
                        <!--[if !mso]><!-->

                        <body class='no-padding' style='margin: 0;padding: 0;-webkit-text-size-adjust: 100%;'>
                          <!--<![endif]-->
                          <table class='wrapper'
                            style='border-collapse: collapse;table-layout: fixed;min-width: 320px;width: 100%;background-color: #fff;'
                            cellpadding='0' cellspacing='0' role='presentation'>
                            <tbody>
                              <tr>
                                <td>
                                  <div role='banner'>
                                    <div class='preheader'
                                      style='Margin: 0 auto;max-width: 560px;min-width: 280px; width: 280px;width: calc(28000% - 167440px);'>
                                      <div style='border-collapse: collapse;display: table;width: 100%;'>
                                        <!--[if (mso)|(IE)]><table align='center' class='preheader' cellpadding='0' cellspacing='0' role='presentation'><tr><td style='width: 280px' valign='top'><![endif]-->
                                        <div class='snippet'
                                          style='display: table-cell;Float: left;font-size: 12px;line-height: 19px;max-width: 280px;min-width: 140px; width: 140px;width: calc(14000% - 78120px);padding: 10px 0 5px 0;color: #adb3b9;font-family: sans-serif;'>

                                        </div>
                                        <!--[if (mso)|(IE)]></td><td style='width: 280px' valign='top'><![endif]-->
                                        <div class='webversion'
                                          style='display: table-cell;Float: left;font-size: 12px;line-height: 19px;max-width: 280px;min-width: 139px; width: 139px;width: calc(14100% - 78680px);padding: 10px 0 5px 0;text-align: right;color: #adb3b9;font-family: sans-serif;'>

                                        </div>
                                        <!--[if (mso)|(IE)]></td></tr></table><![endif]-->
                                      </div>
                                    </div>

                                  </div>
                                  <div>
                                    <div class='layout one-col fixed-width stack'
                                      style='Margin: 0 auto;max-width: 600px;min-width: 320px; width: 320px;width: calc(28000% - 167400px);overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;'>
                                      <div class='layout__inner'
                                        style='border-collapse: collapse;display: table;width: 100%;background-color: #ffffff;'>
                                        <!--[if (mso)|(IE)]><table align='center' cellpadding='0' cellspacing='0' role='presentation'><tr class='layout-fixed-width' style='background-color: #ffffff;'><td style='width: 600px' class='w560'><![endif]-->
                                        <div class='column'
                                          style='text-align: left;color: #8e959c;font-size: 14px;line-height: 21px;font-family: sans-serif;'>

                                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                                            <div style='mso-line-height-rule: exactly;line-height: 50px;font-size: 1px;'>&nbsp;</div>
                                          </div>

                                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                                            <div style='mso-line-height-rule: exactly;mso-text-raise: 11px;vertical-align: middle;'>
                                              <h2
                                                style='Margin-top: 0;Margin-bottom: 0;font-style: normal;font-weight: normal;color: #e31212;font-size: 26px;line-height: 34px;font-family: Avenir,sans-serif;text-align: center;'>
                                                <strong>O que encontramos para voc&#234;, {destinario.Nome}</strong></h2>
                                            </div>
                                          </div>

                                        </div>
                                        <!--[if (mso)|(IE)]></td></tr></table><![endif]-->
                                      </div>
                                    </div>

                                    <div style='mso-line-height-rule: exactly;line-height: 20px;font-size: 20px;'>&nbsp;</div>
                                    <div style='background-color: #fff;'>";    
                                   

            foreach (var produto in produtos)
            {
                htmlEmail += $@"
              <div class='layout two-col stack' style='Margin: 0 auto;max-width: 600px;min-width: 320px; width: 320px;width: calc(28000% - 167400px);overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;'>
                  <div class='layout__inner' style='border-collapse: collapse;display: table;width: 100%;'>
                      <!--[if (mso)|(IE)]><table width='100%' cellpadding='0' cellspacing='0' role='presentation'><tr class='layout-full-width' style='background-color: #fff;'><td class='layout__edges'>&nbsp;</td><td style='width: 300px' valign='top' class='w260'><![endif]-->
                      <div class='column' style='text-align: left;color: #8e959c;font-size: 14px;line-height: 21px;font-family: sans-serif;max-width: 320px;min-width: 300px; width: 320px;width: calc(12300px - 2000%);Float: left;'>

                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                              <div style='mso-line-height-rule: exactly;line-height: 25px;font-size: 1px;'>&nbsp;</div>
                          </div>

                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                              <div style='mso-line-height-rule: exactly;line-height: 15px;font-size: 1px;'>&nbsp;</div>
                          </div>

                      </div>
                      <!--[if (mso)|(IE)]></td><td style='width: 300px' valign='top' class='w260'><![endif]-->
                      <div class='column' style='text-align: left;color: #8e959c;font-size: 14px;line-height: 21px;font-family: sans-serif;max-width: 320px;min-width: 300px; width: 320px;width: calc(12300px - 2000%);Float: left;'>

                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                              <div style='mso-line-height-rule: exactly;line-height: 25px;font-size: 1px;'>&nbsp;</div>
                          </div>
                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                              <div style='font-size: 12px;font-style: normal;font-weight: normal;line-height: 19px;Margin-bottom: 20px;' align='left'>
                                  <img style='border: 0;display: block;height: auto;width: 100%;max-width: 450px;' alt='Plane in flight' width='260' src='{produto.UrlImage}' />
                              </div>
                          </div>
                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                              <div style='mso-line-height-rule: exactly;mso-text-raise: 11px;vertical-align: middle;'>
                                  <h3 style='Margin-top: 0;Margin-bottom: 12px;font-style: normal;font-weight: normal;color: #281557;font-size: 18px;line-height: 26px;font-family: Avenir,sans-serif;'>{produto.Nome}</strong></h3>
                              </div>
                          </div>
                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                              <div style='mso-line-height-rule: exactly;mso-text-raise: 11px;vertical-align: middle;'>
                                  <p class='size-18' style='Margin-top: 0;Margin-bottom: 20px;font-size: 17px;line-height: 26px;' lang='x-size-18'><span style='color:#ff0000'>R$ {produto.Preco}</span></p>
                              </div>
                          </div>
                          <div style='Margin-left: 20px;Margin-right: 20px;'>
                              <div class='btn btn--shadow btn--small' style='text-align:right;'>
                                  <![if !mso]>
                                  <a style='border-radius: 0;display: inline-block;font-size: 11px;font-weight: bold;line-height: 19px;padding: 6px 12px 7px 12px;text-align: center;text-decoration: none !important;transition: opacity 0.1s ease-in;color: #fff !important;box-shadow: inset 0 -2px 0 0 rgba(0, 0, 0, 0.2);background-color: #814fff;font-family: Crete Round, PT Serif, Constantia, Georgia, serif;' href='{produto.UrlProduto}'>Acessar</a><![endif]>
                                  <!--[if mso]><p style='line-height:0;margin:0;'>&nbsp;</p><v:rect xmlns:v='urn:schemas-microsoft-com:vml' href='{produto.UrlProduto}' style='width:73px' fillcolor='#814FFF' stroke='f'><v:shadow on='t' color='#673FCC' offset='0,2px'></v:shadow><v:textbox style='mso-fit-shape-to-text:t' inset='0px,6px,0px,5px'><center style='font-size:11px;line-height:19px;color:#FFFFFF;font-family:Crete Round,PT Serif,Constantia,Georgia,serif;font-weight:bold;mso-line-height-rule:exactly;mso-text-raise:3px'>Book Now</center></v:textbox></v:rect><![endif]-->
                              </div>
                          </div>

                      </div>
                      <!--[if (mso)|(IE)]></td><td class='layout__edges'>&nbsp;</td></tr></table><![endif]-->
                  </div>
              </div>";
                htmlEmail += $@"</div>

                                    <div style='mso-line-height-rule: exactly;line-height: 50px;font-size: 50px;'>&nbsp;</div>

                                    <div role='contentinfo'>
                                      <div class='layout email-footer stack'
                                        style='Margin: 0 auto;max-width: 600px;min-width: 320px; width: 320px;width: calc(28000% - 167400px);overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;'>
                                        <div class='layout__inner' style='border-collapse: collapse;display: table;width: 100%;'>
                                          <!--[if (mso)|(IE)]><table align='center' cellpadding='0' cellspacing='0' role='presentation'><tr class='layout-email-footer'><td style='width: 400px;' valign='top' class='w360'><![endif]-->
                                          <div class='column wide'
                                            style='text-align: left;font-size: 12px;line-height: 19px;color: #adb3b9;font-family: sans-serif;Float: left;max-width: 400px;min-width: 320px; width: 320px;width: calc(8000% - 47600px);'>
                                            <div style='Margin-left: 20px;Margin-right: 20px;Margin-top: 10px;Margin-bottom: 10px;'>

                                              <div style='font-size: 12px;line-height: 19px;'>

                                              </div>
                                              <div style='font-size: 12px;line-height: 19px;Margin-top: 18px;'>

                                              </div>
                                              <!--[if mso]>&nbsp;<![endif]-->
                                            </div>
                                          </div>
                                          <!--[if (mso)|(IE)]></td><td style='width: 200px;' valign='top' class='w160'><![endif]-->
                                          <div class='column narrow'
                                            style='text-align: left;font-size: 12px;line-height: 19px;color: #adb3b9;font-family: sans-serif;Float: left;max-width: 320px;min-width: 200px; width: 320px;width: calc(72200px - 12000%);'>
                                            <div style='Margin-left: 20px;Margin-right: 20px;Margin-top: 10px;Margin-bottom: 10px;'>

                                            </div>
                                          </div>
                                          <!--[if (mso)|(IE)]></td></tr></table><![endif]-->
                                        </div>
                                      </div>
                                    </div>
                                    <div style='line-height:40px;font-size:40px;'>&nbsp;</div>
                                  </div>
                                </td>
                              </tr>
                            </tbody>
                          </table>

                        </body>

                        </html>";
            }


            return htmlEmail;
        }
    }
}
