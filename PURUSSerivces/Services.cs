using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Security.Cryptography;
 

namespace Services
{
    public class svcQuote : IQuote
    {

        public Feedback Add(Quote Quote)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
            {
                DB.Quotes.Add(Quote);
                DB.SaveChanges();
                return new Feedback(true, "Quote successfully added to database");
            }
            }
            catch (Exception E)
            {
                return new Feedback(false, "Unable to add to database");
            }
            
        }

        public Quote GetByID(int ID)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {

                    return DB.Quotes.Where(a => a.ID == ID).SingleOrDefault();
                }
            }
            catch (Exception E)
            {
                return null;
            }
        }

        public Feedback Update(Quote Quote)
        {

            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {
                    Quote UpdateQuote = DB.Quotes.Where(a => a.ID == Quote.ID).SingleOrDefault() ;

                    UpdateQuote.MaximumCoverage = Quote.MaximumCoverage;
                    UpdateQuote.MinimumCoverage = Quote.MinimumCoverage;
                    UpdateQuote.Status = Quote.Status;

                    DB.SaveChanges();

                    return new Feedback(true, "Quote successfully updated");
                }
            }
            catch (Exception E)
            {
                return new Feedback(false, "Unable to update the database");
            }
        }

        public Quote GetByUserID(int ID)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {

                    return DB.Quotes.Where(a => a.Customer.ID == ID).SingleOrDefault();
                }
            }
            catch (Exception E)
            {
                return null;
            }
        }

    }

    public class svcCustomer : ICustomer
    {

        public Feedback Add(Customer Customer)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {
                    Encryption Encryption = new Encryption();
                    Customer.Password = Encryption.Encrypt(Customer.Password, "purus");

                    if (DB.Customers.Where(a => a.Email.ToString().ToUpper() == Customer.Email.ToString().ToUpper()).SingleOrDefault() == null)
                    {
                        DB.Customers.Add(Customer);
                        DB.SaveChanges();

                        return new Feedback(true, "Customer successfully added to database");
                    }
                    else
                    {
                        return new Feedback(false, "Customer already registered");
                    }


                }
            }
            catch (Exception E)
            {
                return new Feedback(false, "Failed to add Customer to the database");
            }
        }

        public Customer GetByID(int ID)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {

                    return DB.Customers.Where(a => a.ID == ID).SingleOrDefault();
                }
            }
            catch (Exception E)
            {
                return null;
            }
        }

        public Feedback Update(Customer Customer)
        {
            return (new Feedback(false, "no implementation"));
        }

        public List<Customer> GetAll(SearchModel SearchModel)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {
                    
                    List<Customer> list = DB.Customers.ToList ();

                    foreach (Customer Customer in list)
                    {
                        if (SearchModel.MinimumCoverage != null & SearchModel.MaximumCoverage != null)
                        {
                            Quote Quote = Customer.Quotes.Where(a => a.CustomerID == Customer.ID).SingleOrDefault();
                            if (Quote != null)
                            {
                                if (!(Quote.MinimumCoverage > SearchModel.MinimumCoverage && Quote.MaximumCoverage < SearchModel.MaximumCoverage))
                                {
                                    list.Remove(Customer);
                                }
                            }
                        }

                        if (SearchModel.InsuranceType != null)
                        {
                            if (Customer.InsuranceType.ToUpper() == SearchModel.InsuranceType.ToUpper())
                            {
                                list.Remove(Customer);
                            }
                        }

                        if (SearchModel.MinAge != null)
                        {
                            if (Customer.Age < SearchModel.MinAge.GetValueOrDefault())
                            {
                                list.Remove(Customer);
                            }
                        }

                        if (SearchModel.MaxAge != null)
                        {
                            if (Customer.Age > SearchModel.MaxAge.GetValueOrDefault())
                            {
                                list.Remove(Customer);
                            }
                        }

                        if (SearchModel.Location != "")
                        {
                            if (!(Customer.City.ToUpper().Contains (SearchModel.Location.ToUpper())))
                            {
                                list.Remove(Customer);
                            }
                        }
                    }

                    return list.Skip(SearchModel.NumberOfResultPerPage * (SearchModel.Page - 1)).Take(SearchModel.NumberOfResultPerPage).ToList();
                }
            }
            catch (Exception E)
            {
                return null;
            }
            
        }
        
        public Feedback Authenticate(Customer Customer)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {
                    Customer customer = DB.Customers.Where(a => a.Email.ToString().ToUpper() == Customer.Email.ToString().ToUpper()).SingleOrDefault();
                    Encryption Encryption = new Encryption();

                    if (Customer.Password == Encryption.Decrypt( customer.Password,"purus") )
                    {
                        return new Feedback(true, "Authenticated");
                    }
                    else 
                    {
                        return new Feedback(false, "Authentiction failed due to wrong password/email.");
                    }
                }
            }
            catch
            {
                return new Feedback(false, "Authentiction failed due to wrong password/email.");
            }
        }
        
        public Customer GetByEmail(string Email)
        {
            try
            {
                using (MyInsuranceDBEntities DB = new MyInsuranceDBEntities())
                {
                    return DB.Customers.Where(a => a.Email.ToString ().ToUpper () == Email.ToString().ToUpper ()).SingleOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }

    }

    public class Encryption 
    {
        public enum Key
        {
            Password,
            SecurityNumber
        }

        public string getRandomKey(Key Key)
        {
            switch (Key)
            {
                case  Key.Password:
                    return createRandomString(8, passwordChars);
                case Key.SecurityNumber:
                    return createRandomString(5, securityNumberChars);
            }

            return null;
        }
        public string createRandomString (int length, string strFeed)
        {
            char[] _out = new char[length];
            
            Random j = new Random(DateTime.Now.Second);

            for (int n = 0; n < length; n++)
            {
                int i = (int)(j.NextDouble() * strFeed.Length);
                if (i == strFeed.Length) { i -= 1; }
                _out[n] = strFeed[i];
            }

            return (new string(_out));
            
        }
        public string Encrypt(string plainText, string passPhrase)
        {
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        private static string passwordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private static string securityNumberChars = "1234567890";

        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        
        public  string Decrypt(string cipherText, string passPhrase)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new System.IO. MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private  byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; 
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }

    public class Feedback
    {
        bool result;
        string message;

        public Feedback(bool Result, string Message)
        {
            this.result = Result;
            this.message = Message;
        }

        public bool Result { get {return result; } set{result=value;}}
        public string Message { get {return message; } set{message=value;}}
    }


}






