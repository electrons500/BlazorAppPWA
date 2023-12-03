using BlazorAppPWA.Shared;

namespace BlazorAppPWA.Server.Service
{
    public class GameService
    {
        private  GamesDBContext.GamesDBContext _context;
        public GameService(GamesDBContext.GamesDBContext context)
        {
            _context = context;
        }

        public  List<Game> games = new()
        {

                new Game() { Id = 1, GameName = "Street Fighter II", Price = 10.0M, CreatedAt = Convert.ToDateTime("2002-06-23") },
                new Game() { Id = 2, GameName = "Mortal Kombat I", Price = 15.0M, CreatedAt = Convert.ToDateTime("1996-04-15") },
                new Game() { Id = 3, GameName = "God of War II", Price = 25.0M, CreatedAt = Convert.ToDateTime("2005-10-04") },
                new Game() { Id = 4, GameName = "FIFA 19", Price = 50.0M, CreatedAt = Convert.ToDateTime("2010-09-02") },
                new Game() { Id = 5, GameName = "Dungeons and Dragons", Price = 23.30M, CreatedAt = Convert.ToDateTime("2003-11-05") }

        };

        public  List<Game> GetGames()
        {
            var gameslist = _context.Games.ToList();
            if (gameslist.Count is 0)
            {
               foreach(var game in games)
                {
                    Game model = new()
                    {
                        Id = game.Id,
                        GameName = game.GameName,
                        Price = game.Price,
                        CreatedAt = game.CreatedAt
                    };
                    _context.Games.Add(model);
                    _context.SaveChanges();
                }

                return _context.Games.ToList();
            }

            return _context.Games.ToList();
        }

        public  bool AddGame(Game model)
        {
            var countgames = _context.Games.ToList();
            Game game = new()
            {
                Id = countgames.Max(x => x.Id) + 1,
                GameName = model.GameName,
                Price = model.Price,
                CreatedAt = model.CreatedAt
            };
            _context.Games.Add(game);
            _context.SaveChanges();
            return true;
        }

        public  Game GetGame(int? id)
        {
            var game = _context.Games.Where(x => x.Id == id).FirstOrDefault();

            return game is null ? new Game() : game;
        }

        public  bool UpdateGame(Game game)
        {
            Game? getgame =  _context.Games.Where(x => x.Id == game.Id).FirstOrDefault();

            getgame!.Id = game.Id;
            getgame.GameName = game.GameName;
            getgame.Price = game.Price;
            getgame.CreatedAt = game.CreatedAt;
            _context.Games.Update(getgame);
            _context.SaveChanges();
            return true;
        }

        public  bool DeleteGame(int id)
        {
            Game? getgame = _context.Games.Where(x => x.Id == id).FirstOrDefault();
            _context.Games.Remove(getgame);
            _context.SaveChanges();
            return true;
        }
    }
}
