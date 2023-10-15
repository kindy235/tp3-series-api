using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.ContentModel;
using SeriesApi.Controllers;
using SeriesApi.Models.EntityFramework;
using SeriesApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeriesApi.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private readonly UtilisateursController _controller;
        private readonly SeriesDbContext _context;
        private readonly IDataRepository<Utilisateur> _dataRepository;

        public UtilisateursControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=seriesDB;uid=postgres;password=admin;");
            _context = new SeriesDbContext(builder.Options);
            _dataRepository = new UtilisateurManager(_context);
            _controller = new UtilisateursController(_dataRepository);
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            // Récupérez la liste des utilisateurs du contexte.
            var expected = _context.Utilisateurs.ToList();

            // Appelez la méthode du contrôleur que vous testez.
            // Act
            var result = _controller.GetUtilisateurs().Result;

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "le type retourné n'est pas un OkObjectResult");
            var okObjectResult = result.Result as OkObjectResult;

            // Assurez-vous que la liste des utilisateurs renvoyée par la méthode est égale à la liste attendue.
            CollectionAssert.AreEqual(expected, okObjectResult?.Value as System.Collections.ICollection, "GetUtilisateurs Test Failed");
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest1()
        {
            // Récupérez la liste des utilisateurs du contexte.
            var expected = _context.Utilisateurs.Where(u=> u.UtilisateurId == 1).FirstOrDefault();

            // Appelez la méthode du contrôleur que vous testez.
            // Act
            var result = _controller.GetUtilisateurById(1).Result;

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "le type retourné n'est pas un OkObjectResult");
            var okObjectResult = result.Result as OkObjectResult;

            Assert.AreEqual(expected, okObjectResult?.Value, "GetUtilisateurById Test Failed");
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest2()
        {
            // Récupérez la liste des utilisateurs du contexte.
            var expected = _context.Utilisateurs.Where(u => u.UtilisateurId == 1).FirstOrDefault();

            // Appelez la méthode du contrôleur que vous testez.
            // Act
            var result = _controller.GetUtilisateurById(2).Result;

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "le type retourné n'est pas un OkObjectResult");
            var okObjectResult = result.Result as OkObjectResult;

            Assert.AreEqual(expected, okObjectResult?.Value, "GetUtilisateurById Test Failed");
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest1()
        {
            // Récupérez la liste des utilisateurs du contexte.
            var expected = _context.Utilisateurs.Where(u => u.Mail.ToUpper().Equals("gdominguez0@washingtonpost.com".ToUpper())).FirstOrDefault();

            // Appelez la méthode du contrôleur que vous testez.
            // Act
            var result = _controller.GetUtilisateurByEmail("gdominguez0@washingtonpost.com").Result;

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "le type retourné n'est pas un OkObjectResult");
            var okObjectResult = result.Result as OkObjectResult;

            Assert.AreEqual(expected, okObjectResult?.Value, "GetUtilisateurByEmail Test1 Failed");
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest2()
        {
            // Récupérez la liste des utilisateurs du contexte.
            var expected = _context.Utilisateurs.Where(u => u.Mail.ToUpper().Equals("gdominguez0@washingtonpost.com".ToUpper())).FirstOrDefault();

            // Appelez la méthode du contrôleur que vous testez.
            // Act
            var result = _controller.GetUtilisateurByEmail("gdominguez0@washingtonpost.com1").Result;

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult), "le type retourné n'est pas un OkObjectResult");
            var okObjectResult = result.Result as OkObjectResult;

            Assert.AreEqual(expected, okObjectResult?.Value, "GetUtilisateurByEmail Test2 Failed");
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS 
            //(=> la décommenter) ou la méthode remove du DbSet
             Utilisateur userAtester = new()
             {
                 Nom = "MACHIN",
                 Prenom = "Luc",
                 Mobile = "1",
                 Mail = "machin" + chiffre + "@gmail.com",
                 Pwd = "Toto1234!",
                 Rue = "Chemin de Bellevue",
                 CodePostal = "74940",
                 Ville = "Annecy-le-Vieux",
                 Pays = "France",
                 Latitude = null,
                 Longitude = null
             };

            Regex regex = new (@"^0[0-9]{9}$");
            if (!regex.IsMatch(userAtester.Mobile))
            {
                _controller.ModelState.AddModelError("Mobile", "Le n° de mobile doit contenir 10 chiffres"); //On met le même message que dans la classe Utilisateur.
            }

            // Act
            var result = _controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière 
            //synchrone, afin d’attendre l’ajout
             // Assert

            Utilisateur? userRecupere = _context.Utilisateurs
                .Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper())
                .FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
             
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
                    Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
         }
    }
}
