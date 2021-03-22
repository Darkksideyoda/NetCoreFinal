using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalMVC.Models;
using System.Diagnostics;
using System.Threading;

namespace FinalMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult AdminView(authenticateUser Admin)
        {
            return View(Admin);
        }

        public IActionResult StandartUserView(authenticateUser standartUser){

            return View(standartUser);
        }

      


        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(authenticateUser User)
        {
          
            var adminRepository = new adminRepo();
            var standartUser = new standartUserRepo();




           
            ////////////////////////STANDART USER DATA BIND BLOGU BASLANGIC//////////////////////////////
            var collectionStandartUserUsernameData = standartUser.standartUserUsername; //standart User Username toplar.
            string findStandartUserUsername = User.username;                            //Getirilen Degeri Stringe atar:
            /////////////////////////////////////////////////////////////////////////////////////////////
            var collectionStandartUserPasswordData = standartUser.standarUserPassword;  //Standart User Password Toplar.
            string findStandartUserPassword = User.password;                            //Getirilen Degeri Stringe Atar.
            /////////////////////////////////////////////////////////////////////////////////////////////
            string resultStandartUserUsername = Array.Find(collectionStandartUserUsernameData,element =>element == findStandartUserUsername);//Standart User Username array icerisinde girilen input arar.Ve geri deger dondurur.
            int indexResultStandartUserUsername=Array.FindIndex(collectionStandartUserUsernameData,element => element == findStandartUserUsername);//Bulunmasi durumunda index degeri doner bulamazssa -1 degeri doner.
            
            string resultStandartUserPassword = Array.Find(collectionStandartUserPasswordData,element =>element == findStandartUserPassword);//Standart User Password - String
            int indexResultStandartUserPassword = Array.FindIndex(collectionStandartUserPasswordData,element => element == findStandartUserPassword);//Standar User Password - Index
            ////////////////////////STANDART USER DATA BIND BLOGU BASLANGIC//////////////////////////////
           






           
            ////////////////////////ADMIN DATA BIND BLOGU BASLANGIC//////////////////////////////
            var collectAdminUsernameData = adminRepository.adminUsername;//admin username toplar.
            string findAdminUsername = User.username; // getirilen degeri stringe atar.
            /////////////////////////////////////////////////////////////////////////////////////
            var collectAdminPasswordData = adminRepository.adminPassword; // admin password toplar.
            string findAdminPassword = User.password;                      //getirilen degeri password stringine atar.
            /////////////////////////////////////////////////////////////////////////////////////
            string resultAdminUsername = Array.Find(collectAdminUsernameData, element => element == findAdminUsername);  //array icerisinde arama yapar ve bulmasi durumunda degeri dondurur -Admin -String- Username
            int indexResultAdminUsername = Array.FindIndex(collectAdminUsernameData,element => element == findAdminUsername); //array icerisinde arama yapar ve bulunmasi durumunda indexi dondurur.-Admin -Index - Username
            
            string resultAdminPassword = Array.Find(collectAdminPasswordData, element => element == findAdminPassword);//Password-String-Admin
            int indexResultAdminPassword = Array.FindIndex(collectAdminPasswordData,element => element == findAdminPassword);//Password-Index-Admin
            //////////////////////////////ADMIN DATA BIND BLOGU BITIS////////////////////////////
            
            



             Debug.WriteLine("INDEX ADMIN username :"+indexResultAdminUsername+"\nINDEX admin password:"+indexResultAdminPassword);
             Debug.WriteLine("INDEX USER username :"+indexResultStandartUserUsername+"\nINDEX USER password:"+indexResultStandartUserPassword);
            
            

            if(String.IsNullOrEmpty(User.username) || string.IsNullOrEmpty(User.password)){//Eger Tum Inputlar Bos ise donecek olan condition.
                  
                 return Content("KULLANICI ADI VEYA SIFRE BOS GECILEMEZ LUTFEN DONUP TUM ALANLARI DOLDURUNUZ ");        //Content Return.
            }

            
            
            if(indexResultAdminUsername == indexResultAdminPassword && indexResultAdminUsername != -1 && indexResultAdminPassword != -1){   //Eger Inputlara girilen degerler aranan dizide bulunursa ve gerekli sartlari saglarsa AdminPage Return olacak.


                User.username = adminRepository.adminUsername[indexResultAdminUsername];
                User.password = adminRepository.adminPassword[indexResultAdminUsername];
                User.name = adminRepository.adminName[indexResultAdminUsername];
                User.surname = adminRepository.adminSurname[indexResultAdminUsername];
                User.mail = adminRepository.adminMail[indexResultAdminUsername];
                User.adres = adminRepository.adminAdres[indexResultAdminUsername];
                User.phone = adminRepository.adminPhone[indexResultAdminUsername];
                User.age = adminRepository.adminAge[indexResultAdminUsername];
                User.job = adminRepository.adminJob[indexResultAdminUsername];
                return View("AdminView",User);
            
            }

            if(indexResultStandartUserUsername == indexResultStandartUserPassword && indexResultStandartUserUsername !=-1 && indexResultStandartUserPassword != -1){ //Admin Dizide bulunmazssa UserRepoda bulunuyor mu diye kontrol ediyoruz. Eger bulunursa gerekli Sayfayi return ediyoruz.
                
                User.username = standartUser.standarUserName[indexResultStandartUserUsername];
                User.password = standartUser.standarUserPassword[indexResultStandartUserUsername];
                User.name = standartUser.standarUserName[indexResultStandartUserUsername];
                User.surname = standartUser.standarUserSurname[indexResultStandartUserUsername];
                User.mail = standartUser.standartUserMail[indexResultStandartUserUsername];
                User.adres = standartUser.standartUserAdres[indexResultStandartUserUsername];
                User.phone = standartUser.standartUserPhone[indexResultStandartUserUsername];
                User.age = standartUser.standartUserAge[indexResultStandartUserUsername];
                User.job = standartUser.standartUserJob[indexResultStandartUserUsername];
                return View("StandartUserView",User);
            }


            return Content("HATALI KULLANICI ADI VEYA ŞİFRE DÖNÜP TEKRAR DENEYİNİZ !"); //Eger tum inputlar dolu fakat tum index degerleri -1 donerse hatali giris parametreleri oldugundan gerekli contenti return ediyoruz.

          



        }
    }
}
