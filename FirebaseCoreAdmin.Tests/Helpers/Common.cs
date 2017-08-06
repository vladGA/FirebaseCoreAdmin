using FirebaseCoreAdmin.Extensions;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
namespace FirebaseCoreAdmin.Tests.Helpers
{
    public static class Common
    {
        public static readonly string testRSAKey = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCfWfB7xe2L/MnE\n524W/h/1svdcru1fLM8KTMqBP5EbbZF9MPKnF7GzYdwwCK+e6gNaPQHCBSTRjjEU\nH8CFRmxviNmXHGT/RJHC6ilOK3A2dEuXc9ssDkeg8wt99YdbOoIKNAUgrcwv4mDi\ndo97fxgNBLnzCBeS9H/HiQTglhAOjLJF6Aa7Ymdbe5tuVk7vIPREuGKW+MYz8PVF\nfvTjRb+iCZ+Se6Fh1qu8Na38x17IP583WhuHY/CMW5oG9Kdlfe4ZSaU5uXR4EESv\nWkxv/Y0lM1YOhPXtOVvfiutRbm1k0fGdPU1kz6EH0yb3ZdJlvILchxYh399vBhLb\n53D9eIL3AgMBAAECggEABtq0iG5sGGBoQWr/BkgqEcR/9WGk3josMbI557NXR4m/\n/1WSqnNPdnrYIMzO2RQRztje8XyNJ+Jo5Ae1nUX5Nhb53REgmwZVCsBNxIoDyqPX\n+IzIOa3nkNnDfciI6PisTUoNimlPZiPjrPoSk+pUm4K/iN+pO+3+bRPMvOhXinib\ndTGY+SWp8OpiIMUhio8ewrGgSq7VJ/Jz2l7nYAr/zxEb9BcI1vhP1VTXg1UAQhd6\nwERm/yxhJlX72v6ai9XDy1m4Asiv8mFXZ99x5ciPAQRVbrQTvqB8y3f5TeDog2VF\nJQ3l6Kmq11NZPWRzQ+i1ZxKBtzZ9DJ5wDUTe7n5TCQKBgQDOh5d/WHVRiD6yZap3\nhjLI3WC2xr7unHNhBZkB1+2DN/DbZYhS32pYfP8bByS7dDifhFBYVpWph1/BiOH6\npHJ0SN/5z+DAOTBZgquMtAn4Sf2E/YEC0+EwpJklaFfFqzVmOeG8cMUEMHugBWjM\nGVk0irXU2Uwzta3ALZIP1X8xTQKBgQDFhV7Fbzf/V4Wlox6ZmEY6ftPnW7PRWICY\nFGvUcgT5etBL1MXQ6mO3Q4LHnWjrJwIb9wrrRwVHxZWqFt5RW2XG50A58XTs/0el\nUgUh4bAgkHDlCrP+ejhYo/HLMObaMGnptnJ0S+U+NyG3Ai5d5lE0uvyAPe7p1HQk\nNjIgSHYjUwKBgQCW2TqybO3jy1vf+Zn1CzP0up11Ytz6c5NifjAvxINaoHVwCkz0\nGvMWBtZd623M7SqeeIomu7c8yAAM3+oOpCZlAQV29Xr3a84A0wxyDN5rV7+wb2jX\no+KLIO4rAHd7jX3HWOekN8nvMWpaixjsWmdplRcSjFS4QC7Ue3R/1DS+0QKBgHXt\nYt/Nv0kcIBb8hNB/Ma7K+gkvnvRmOqCR0K9OQ9oPkT01E8XfqxlHnVVAjduXtpKr\ntEE0bdqkBO9AKAu63dEbUCVIG99DqqrxhU7tSBKpKj4VXdGZdvq1Jy+BStMKBLIc\n08jLZ1r135M8IiK91837tbV7RKEmzxCvqs3LdX0xAoGAMPFFsvtGcpec7qh0P5DR\nxf0kcU5aPZ70MTn8oisv9P8er+u0CEUPGPYMV337NpsouO1O6szCEnXonRQy/C+q\niII1mwHD2rGVyY8MukiDcVEoEin5IRKw4HMS1gHWi/WCX0sY52Y9FY5gK6TXmp88\nE6/VGU59v2nf9ie+HcZR9Z0=\n-----END PRIVATE KEY-----\n";
        public static RSAParameters MockRSAParams
        {
            get
            {
                string jwt = string.Empty;
                RsaPrivateCrtKeyParameters key;
                using (StringReader sr = new StringReader(testRSAKey))
                {
                    PemReader pr = new PemReader(sr);
                    key = (RsaPrivateCrtKeyParameters)pr.ReadObject();
                }

                var rsaParams = key.ToRSAParameters();
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaParams);
                    return rsa.ExportParameters(true);
                }
                
            }
        }
    }
}
