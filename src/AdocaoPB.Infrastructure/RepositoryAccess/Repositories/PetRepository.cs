using Microsoft.EntityFrameworkCore;
using AdocaoPB.Domain.Entities;
using AdocaoPB.Domain.Repositories.RepositoryPet;

namespace AdocaoPB.Infrastructure.RepositoryAccess.Repositories;

public class PetRepository : 
    IPetReadOnlyRepository,
    IPetWriteOnlyRepository, 
    IPetUpdateOnlyRepository
{

    private readonly AdocaoPBContext _context;

    public PetRepository(AdocaoPBContext context) {
        _context = context;
    }

    public async Task Add(Pet pet) {
        await _context.Pets.AddAsync(pet);
    }

    public async Task<IList<Pet>> GetAll(
        int pageNumber,
        int pageQuantity
    ){
        return await _context.Pets.Include(pet => pet.Owner)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageQuantity)
            .Take(pageQuantity)
            .ToListAsync();
    }

    public async Task<IList<Pet>> GetAllPetsForAdopter(
        string idAdopter
    ){
        return await _context.Pets
            .AsNoTracking()
            .Where(pet => pet.AdopterId.Equals(idAdopter))
            .Include(pet => pet.Owner)
            .ToListAsync();
    }

    public async Task<IList<Pet>> GetAllPetsForUser(string idOwner) {
        return await _context.Pets
            .AsNoTracking()
            .Where(pet => pet.OwnerId.Equals(idOwner))
            .Include(pet => pet.Adopter)
            .ToListAsync();
    }

    public async Task<Pet> GetById(long idPet) {
        //string query = $"SELECT Pet.*, EndUser.*, Us.* FROM Pets Pet INNER JOIN AspNetUsers Us ON Pet.OwnerId = Us.Id  LEFT JOIN EnderecoUsers EndUser ON EndUser.UserId = Us.Id WHERE Pet.Id = {idPet}";        

        return await _context.Pets.AsNoTracking()
        .Where(pet => pet.Id == idPet)
        .Include(pet => pet.Owner)
            .ThenInclude(owner => owner.EnderecoUser)
        .FirstOrDefaultAsync();
    }

    public async Task<Pet> RecuperarPetPorIdUpdate(long idPet) {
        return await _context.Pets.FirstOrDefaultAsync(pet => pet.Id == idPet);
    }

    public void AdoptionPet(Pet pet) {
        _context.Update(pet);
    }

    public void ConcludeAdoptionPet(Pet pet) {
        _context.Update(pet);
    }

    public async Task Delete(long idPet) {
        var pet = await _context.Pets.
            FirstOrDefaultAsync(pet => pet.Id == idPet);

        _context.Pets.Remove(pet);
    }

    public void Update(Pet pet) {
        _context.Pets.Update(pet);
    }
}
