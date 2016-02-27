﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shooter.MapClasses;
using Shooter.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Shooter.Entities {
    class Character : Entity {
        private int health;
        private int stamina;
        private Weapon weapon;

        public Character(ContentManager content) : base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;

            //default character creates default weapon which is a pistol
            weapon = new Weapon(content);
            //Set Health
            health = 1;
            //set stamina
            stamina = 0;
        }

        //properties
        public int Health {
            get { return health; }
            set { health = value; }
        }
        public int Stamina {
            get { return stamina; }
            set { stamina = value; }
        }
        public Weapon Weapon {
            get {
                return weapon;
            }
        }

        public Character(ContentManager content, double x, double y, string t): base(content, x, y, t) {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }
            //set coordinates
            loc.X = x;
            loc.Y = y;
            direction = 0.0;

            //non-collidable object by default
            collision = false;

            //Set health
            health = 1;
        }
        
        public Character(ContentManager content, double x, double y, double dir, string t, bool c): base(content, x, y, t) {
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }

            //Set health
            health = 1;

            loc.X = x;
            loc.Y = y;
            collision = c;
            if (dir > 360 || dir < 0) {
                direction = 0;
            } else {
                direction = dir;
            }
        }
        public Projectile Shoot(ContentManager content) {
            Projectile p = new Projectile(content, loc.X, loc.Y, this.Direction + weapon.GetSpread()*(Math.PI/180.0), 10.0, "Bullet", true);
            return p;
        }

        public bool CheckHealth(){
            //True if the target is still alive
            if(health > 0){
                return true;
            }
            //False if the target is dead
            return false;
        }
    }
}
