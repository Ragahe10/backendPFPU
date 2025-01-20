namespace backendPFPU.Models;

public class Usuario{
    private int id_usuario;
    private int dni;
    private string contrasenia;
    private string correo;
    private string nombre;
    private string apellido;

    public int Id_usuario { get => id_usuario; set => id_usuario = value; }
    public int Dni { get => dni; set => dni = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public string Correo { get => correo; set => correo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apellido { get => apellido; set => apellido = value; }
}