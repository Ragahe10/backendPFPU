namespace backendPFPU.Models;

public class Usuario{
    protected int id_usuario;
    protected int dni;
    protected string contrasenia;
    protected string correo;
    protected string nombre;
    protected string apellido;
    protected string tipo;
    protected int activo;
    protected string telefono;

    public int Id_usuario { get => id_usuario; set => id_usuario = value; }
    public int Dni { get => dni; set => dni = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public string Correo { get => correo; set => correo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
    public string Tipo { get => tipo; set => tipo = value; }
    public int Activo { get => activo; set => activo = value; }
    public string Telefono { get => telefono; set => telefono = value; }
}