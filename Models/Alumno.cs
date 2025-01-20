namespace backendPFPU.Models;

public class Alumno : Usuario {
    private string direccion;
    private int matricula;
    private DateTime fecha_nac;

    public Alumno()
    {
    }

    public Alumno(Usuario usuario, string direccion, int matricula, DateTime fecha_nac)
    {
        this.id_usuario = usuario.Id_usuario;
        this.dni = usuario.Dni;
        this.contrasenia = usuario.Contrasenia;
        this.correo = usuario.Correo;
        this.nombre = usuario.Nombre;
        this.apellido = usuario.Apellido;
        this.tipo = usuario.Tipo;
        this.direccion = direccion;
        this.matricula = matricula;
        this.fecha_nac = fecha_nac;
    }

    public string Direccion { get => direccion; set => direccion = value; }
    public int Matricula { get => matricula; set => matricula = value; }
    public DateTime Fecha_nac { get => fecha_nac; set => fecha_nac = value; }
}