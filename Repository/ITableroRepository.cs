using EspacioTablero;

namespace EspacioRepositorios
{
    public interface ITableroRepository{
        public void CrearTablero(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public Tablero GetById(int id);
        public List<Tablero> GetTableroDondeTengoTareas(int idUsuario);
        public List<Tablero> GetTableroUsuario(int idUsuario);
        public List<Tablero> GetTodos();
        public void RemoveTableroUsuario(int idUsuario);
        public void Remove(int id);
    }
}