using EspacioTablero;

namespace EspacioRepositorios
{
    public interface IUsuarioRepository
    {
        public List<Usuario> GetAll();
        public void Create(Usuario usu);
        public void Update(int id, Usuario usuario);
        public Usuario GetById(int id);
        public void Remove(int id);


    }
}