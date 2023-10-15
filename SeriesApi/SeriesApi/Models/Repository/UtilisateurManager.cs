﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Models.EntityFramework;

namespace SeriesApi.Models.Repository
{
    public class UtilisateurManager : IDataRepository<Utilisateur>
    {
        private readonly SeriesDbContext seriesDbContext;

        public UtilisateurManager() { }

        public UtilisateurManager(SeriesDbContext context)
        {
            seriesDbContext = context;
        }

        public async Task AddAsync(Utilisateur utilisateur)
        {
            await seriesDbContext.Utilisateurs.AddAsync(utilisateur);
            await seriesDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Utilisateur utilisateur)
        {
            seriesDbContext.Utilisateurs.Remove(utilisateur);
            await seriesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAllAsync()
        {
            return await seriesDbContext.Utilisateurs.ToListAsync();
        }

        public async Task<ActionResult<Utilisateur>> GetByIdAsync(int id)
        {   
            return await seriesDbContext.Utilisateurs.FindAsync(id);
        }

        public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
        {
            return await seriesDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }

        public async Task UpdateAsync(Utilisateur utilisateur, Utilisateur entity)
        {
            seriesDbContext.Entry(utilisateur).State = EntityState.Modified;
            utilisateur.UtilisateurId = entity.UtilisateurId;
            utilisateur.Nom = entity.Nom;
            utilisateur.Prenom = entity.Prenom;
            utilisateur.Mail = entity.Mail;
            utilisateur.Rue = entity.Rue;
            utilisateur.CodePostal = entity.CodePostal;
            utilisateur.Ville = entity.Ville;
            utilisateur.Pays = entity.Pays;
            utilisateur.Latitude = entity.Latitude;
            utilisateur.Longitude = entity.Longitude;
            utilisateur.Pwd = entity.Pwd;
            utilisateur.Mobile = entity.Mobile;
            utilisateur.NotesUtilisateur = entity.NotesUtilisateur;
            await seriesDbContext.SaveChangesAsync();
        }
    }
}
