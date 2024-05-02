using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.IO;
using System.Linq;
//using Newtonsoft.Json;
using TiledCS;

namespace TiledSFMLMapExample
{
    class Program
    {
        //altezza e larghezza della finestra
        const int WIDTH = 640;
        const int HEIGHT = 480;
        const string TITLE = "TiledCS Map Rendering with SFML!";


        static Texture texture = new Texture(@"..\..\..\gfx\map\exampleTileset.png");
        //creo uno sprite associando la texture appena caricata
        static Sprite sprite = new Sprite(texture);

        static void Main(string[] args)
        {
            //impostazioni finestra
            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE);
            window.SetVerticalSyncEnabled(true);

            // For loading maps in XML format
            var map = new TiledMap(@"..\..\..\gfx\map\exampleMap.tmx");
            //TiledTileset tileset = new TiledTileset(@"C:\Users\nikof\source\repos\TiledSFMLMapExample\TiledSFMLMapExample\resources\gfx\map\exampleTileset.tsx");
            var tilesets = map.GetTiledTilesets(@"..\..\..\gfx\map/"); // DO NOT forget the / at the end

            // Retrieving objects or layers can be done using Linq or a for loop
            var firstLayer = map.Layers[0];

            // contiene il tipo di layer (in questo caso "TileLayer")
            Console.WriteLine(firstLayer.type);

            // dal github del progetto:
            // A gid is a tile index from a tileset, not the tile id as some think. In order to render a specific
            // tile from a tileset into your spritebatch, you would need to link the gid to a tileset
            // stampa tutti i gid presenti nel primo layer
            foreach (int gid in firstLayer.data)
            {
                Console.WriteLine(gid);
            }

            //lambda expression per prendere i layer di tipo TileLayer, ovvero quelli che vanno renderizzati
            var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

            //loop principale
            while (window.IsOpen)
            {
                //gestione degli eventi
                window.DispatchEvents();

                //pulizia della finestra
                window.Clear(Color.Black);

                foreach (var layer in tileLayers)
                {
                    Console.WriteLine("Layer {0}", layer.id);
                    for (var y = 0; y < layer.height; y++)
                    {
                        for (var x = 0; x < layer.width; x++)
                        {
                            var index = (y * layer.width) + x; // Assuming the default render order is used which is from right to bottom
                            var gid = layer.data[index]; // The tileset tile index
                            var tileX = (x * map.TileWidth);
                            var tileY = (y * map.TileHeight);

                            // Gid 0 is used to tell there is no tile set
                            if (gid == 0)
                            {
                                continue;
                            }

                            // Helper method to fetch the right TieldMapTileset instance. 
                            // This is a connection object Tiled uses for linking the correct tileset to the gid value using the firstgid property.
                            var mapTileset = map.GetTiledMapTileset(gid);

                            // Retrieve the actual tileset based on the firstgid property of the connection object we retrieved just now
                            var tileset = tilesets[mapTileset.firstgid];

                            // Use the connection object as well as the tileset to figure out the source rectangle.
                            var rect = map.GetSourceRect(mapTileset, tileset, gid);

                            // Render sprite at position tileX, tileY using the rect
                            //Console.Write("tileX:{0},tileY:{1}|rect.x:{2},rect.y:{3}\t", tileX, tileY, rect.x, rect.y);
                            sprite.Position = new Vector2f(tileX, tileY);
                            sprite.TextureRect = new IntRect(rect.x, rect.y, rect.width, rect.height);

                            //disegna lo sprite
                            window.Draw(sprite);

                            //Console.ReadKey();

                        }
                        //Console.WriteLine();
                    }
                }

                //visualizza i contenuti disegnati
                window.Display();
            }


        }
    }
}
