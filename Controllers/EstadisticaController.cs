﻿using backendPFPU.Models;
using backendPFPU.Repositories;
using backendPFPU.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendPFPU.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadisticaController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
         private readonly ICursoRepository _cursoRepository;
       private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IDeudaRepository _deudaRepository;

        public EstadisticaController(IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository, IMateriaRepository materiaRepository, IAsistenciaRepository asistenciaRepository, IDeudaRepository deudaRepository)
    {
        _usuarioRepository = usuarioRepository;
        _cursoRepository = cursoRepository;
            _asistenciaRepository = asistenciaRepository;
            _materiaRepository = materiaRepository;
            _deudaRepository = deudaRepository;
        }
    
        [HttpGet]
        [Route("/estadisticasAdmin")]
        public IActionResult GetEstadisticasAdmin()
        {
            var statsAdmin = new EstadisticasAdmin();
            statsAdmin.cantidadAlumnos = _usuarioRepository.GetAlumnos().Count;
            statsAdmin.cantidadDocentes = _usuarioRepository.GetDocentes().Count;
            statsAdmin.cantidadMaterias = _materiaRepository.GetMaterias().Count;
            statsAdmin.cantidadCursos = _cursoRepository.GetAll().Count;
            statsAdmin.PorcentajeAsistenciasGlobal = _asistenciaRepository.GetPorcentajeAsistenciasGlobal();
            statsAdmin.AlumnosConDeuda = _deudaRepository.GetCantidadAlumnosConDeuda();

            return Ok(statsAdmin);
        }

        [HttpGet]
        [Route("/estadisticasDocente/{id}")]
        public IActionResult GetEstadisticasDocente(int id)
        {
            var statsDocente = new EstadisticasDocente();
            statsDocente.cantidadAlumnos = _usuarioRepository.GetAlumnosByDocente(id).Count;
            statsDocente.cantidadMaterias = _materiaRepository.GetMateriasByDocente(id).Count;
           
            return Ok(statsDocente);
        }

        [HttpGet]
        [Route("/estadisticasAlumno/{id}")]
        public IActionResult GetEstadisticasAlumno(int id)
        {
            var statsAlumno = new EstadisticasEstudiante();
          
            statsAlumno.cantidadMaterias = _materiaRepository.GetCantidadMateriasByAlumno(id);
            statsAlumno.porcentajeAsistencia = _asistenciaRepository.GetPorcentajeAsistenciasByAlumno(id);
            return Ok(statsAlumno);
        }

        [HttpGet]
        [Route("/graficosAdmin/asistencia")]
        public IActionResult GetGraficoAsistenciaAdmin()
        {
            return Ok(_asistenciaRepository.GetGraficoAsistenciaAdmin());
        }
    }
}
