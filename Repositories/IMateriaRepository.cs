﻿using backendPFPU.Models;
namespace backendPFPU.Repositories
{
    public interface IMateriaRepository
    {
        List<Materia> GetMaterias();
        Materia GetMateria(int id);
        void AddMateria(Materia materia);
        void UpdateMateria(Materia materia);
        void DeleteMateria(int id);
    }
}
