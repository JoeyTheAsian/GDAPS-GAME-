﻿using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Shooter.Entities;
using Shooter.MapClasses;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter.Controls {
    static class Shooting {
        /* Old Code just in case
        if (player.Weapon.Auto) {
                    if (oldMState.LeftButton == ButtonState.Pressed) {
                        if (temp) {
                            SoundEffect TempSound;
                            //enqueue gunshot sound
                            //only shoot if not a null projectile
                            Projectile p = player.Weapon.Shoot(Content, player, c, m.TileSize);
                            if (p != null) {
                                projectiles.Add(p);
                                soundEffects.TryGetValue("gunshot", out TempSound);
                                curSounds.Enqueue(TempSound);
                                c.screenShake = true;
                            } else {
                                player.Weapon.Shoot(Content, player, c, m.TileSize);
                                //enqueue gun click sound if empty
                                soundEffects.TryGetValue("emptyClick", out TempSound);
                                curSounds.Enqueue(TempSound);
                            }
                        }
                    }
                } else {
                    if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton != ButtonState.Pressed) {
                        if (temp) {
                            SoundEffect TempSound;
                            //enqueue gunshot sound
                            //only shoot if not a null projectile
                            Projectile p = player.Weapon.Shoot(Content, player, c, m.TileSize);
                            if (p != null) {
                                projectiles.Add(p);
                                soundEffects.TryGetValue("gunshot", out TempSound);
                                curSounds.Enqueue(TempSound);
                                c.screenShake = true;
                            } else {
                                player.Weapon.Shoot(Content, player, c, m.TileSize);
                                //enqueue gun click sound if empty
                                soundEffects.TryGetValue("emptyClick", out TempSound);
                                curSounds.Enqueue(TempSound);
                            }
                        }
                    }
                }
                */

        //Static array for the weapons in the game
        public static Weapon[] weapons = new Weapon[2];
        public static Texture2D[] weaponsFaces = new Texture2D[2];

        //Creates the wepaons that are int the game
        public static void CreateWeapons(ContentManager Content) {
            //Adds the pistol
            weapons[0] = new Weapon(Content);
            weaponsFaces[0] = Content.Load<Texture2D>("Pistol_Player");
            //Adds the Tommy Gun
            weapons[1] = new Weapon(Content, true, 10, 14, "SubmachineGun", 2, "Ammo", "Submachine Gun", 15, 4, 1800);
            weaponsFaces[1] = Content.Load<Texture2D>("Submachine_Gun_Player");
        }

        //Shoots the player's current gun
        public static void ShootWeapon(Character player, MouseState mState, MouseState oldMState, List<Projectile> projectiles, bool temp, Camera c, ContentManager Content, Queue<SoundEffect> curSounds, Dictionary<string, SoundEffect> soundEffects,ref Map m) {
            if (player.Weapon.Auto) {
                if (oldMState.LeftButton == ButtonState.Pressed) {
                    if (temp) {
                        SoundEffect TempSound;
                        //enqueue gunshot sound
                        //only shoot if not a null projectile
                        Projectile p = player.Weapon.Shoot(Content, player, c, m.TileSize);
                        if (p != null) {
                            //create new projectile
                            projectiles.Add(p);
                            //load and enqueue sound
                            soundEffects.TryGetValue("gunshot", out TempSound);
                            curSounds.Enqueue(TempSound);
                            //add the player's sound to the map's sound queue for AI to detect
                            m.sounds.Add(player.Loc);
                            c.screenShake = true;
                        } else {
                            player.Weapon.Shoot(Content, player, c, m.TileSize);
                            //enqueue gun click sound if empty
                            soundEffects.TryGetValue("emptyClick", out TempSound);
                            curSounds.Enqueue(TempSound);
                        }
                    }
                }
            } else {
                if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton != ButtonState.Pressed) {
                    if (temp) {
                        SoundEffect TempSound;
                        //enqueue gunshot sound
                        //only shoot if not a null projectile
                        Projectile p = player.Weapon.Shoot(Content, player, c, m.TileSize);
                        if (p != null) {
                            projectiles.Add(p);
                            soundEffects.TryGetValue("gunshot", out TempSound);
                            curSounds.Enqueue(TempSound);
                            c.screenShake = true;
                        } else {
                            player.Weapon.Shoot(Content, player, c, m.TileSize);
                            //enqueue gun click sound if empty
                            soundEffects.TryGetValue("emptyClick", out TempSound);
                            curSounds.Enqueue(TempSound);
                        }
                    }
                }
            }
        }

        //Switches the player's weapon
        public static void SwitchWeapon(Character player, KeyboardState state, KeyboardState oldState) {
            //Press E to switch the player's weapon
            if (state.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E)) {
                //Gets the index of the player's current weapon
                int index = 0;
                for (int i = 0; i < weapons.Length; i++) {
                    if (weapons[i].Name.Equals(player.Weapon.Name)) {
                        //Sets the new index to the old one plus one and breaks the loop
                        index = i + 1;
                        break;
                    }
                }

                //Makes sure there is a weapon in the next slot or sets it to the first slot
                if (index == weapons.Length) {
                    index = 0;
                }

                //Changes the weapon
                player.Weapon = weapons[index];
                player.EntTexture = weaponsFaces[index];
            }
        }
    }
}
