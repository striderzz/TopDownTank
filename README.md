# Top Down Tank Controller

![Gameplay Screenshot](screenshots/screenshot.png)

--

## Overview

**Tank Battleground** is a simple top-down arena‐style tank shooter developed in Unity. Players control a green tank and must navigate a grid‐based battlefield, avoid obstacles, and destroy enemy vehicles (represented by blue cars) using projectile‐based weapons. The environment contains destructible and static obstacles (white cube blocks), dynamic AI targets, and visual effects (explosions, health indicators).  

This repository contains all the project files, scripts, scene prefabs, and asset configurations necessary to build and run the game as seen in the screenshot above.

---

## Features

- **Player Tank Controls**  
  - Smooth movement & rotation using Rigidbody physics.  
  - Turret aiming toward target position.  
  - Projectile firing with collision detection.  

- **Health System**  
  - Health bar slider (UI Image Fill) above the player tank.  
  - Damage feedback: flashing or particle effect on hit.  
  - Destroy player tank on health depletion.  

- **Visual & Audio Effects**  
  - Explosion particle system when enemies or obstacles are destroyed.  
  - Muzzle flash and shell tracers for tank cannon.  
  - (Optional) Background music and impact sounds.  

- **Modular Code Architecture**  
  - Clean separation of scripts for movement, shooting, health management, and AI behavior.  
  - Easy to extend: add more enemy types, power‐ups, or level variants.  



---


