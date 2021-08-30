using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Folder.Heroes
{
    class World
    {

        const int MAP_HEIGHT = 8;
        const int MAP_WIDTH = 8;

        _Object[, ] map;

        public World() {
            map = new _Object[MAP_HEIGHT, MAP_WIDTH];
        }

        public void Update() {
            for (int i = 0; i < MAP_HEIGHT; i++)
                for (int j = 0; j < MAP_WIDTH; j++)
                    map[i, j].Update();
            
        }

        private int GetValue(int value, int minValue, int maxValue, int offset) {
            if (value + offset < minValue)
                return minValue;
            if (value + offset > maxValue)
                return maxValue;
            return value + offset;
        }
    }
}
