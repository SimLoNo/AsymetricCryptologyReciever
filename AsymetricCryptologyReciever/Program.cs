using AssymetricCyptologyReciever;
using System;
using System.Text;

var rsa = new RsaWithXmlKey();

const string publicKeyPath = "c:\\temp\\publickey.xml";
const string privateKeyPath = "c:\\temp\\privatekey.xml";
rsa.AssignKey(publicKeyPath, privateKeyPath);

Console.WriteLine(File.ReadAllText(publicKeyPath));
Console.ReadLine();

Console.WriteLine("Indtast den krypterede besked.");
string encryptedMessage = Console.ReadLine();
byte[] decryptedBytes = rsa.Decrypt(privateKeyPath, encryptedMessage);
Console.WriteLine("Decrypted besked:");
Console.WriteLine(Encoding.UTF8.GetString(decryptedBytes));