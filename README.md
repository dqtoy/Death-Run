Death Run
Game Features: 
2D infinite runner
Player can jump to avoid death traps by tapping on the screen
Player can collect stars to gain fuel
When there's fuel player can Tap and Hold to fly

Project Features:
Singleton Scripts: GameManager, AudioManager, UIManager, ObjectPooler, Tile Spawner, ParticleEffect
GameManager controls Player Health values, player collections and central calculations 
AudioManager plays all the audio. It uses a helper Sound class which is used a [System.Seriazable] so that all the sounds needed along with volume and pitch property can be provided from the editor.
UIManager has all the UI GameObjects in it. It takes data from the GameManager and shows on the screen.
ObjectPooler has a helper class Pool which is [System.Seriazable]. Pool takes tag, prefab and size as input. ObjectPooler instantiates Pools for different unique GameObjects and it Instantiates the 'Pool.size' number of its instances when the game starts and makes their SetActive property to false. When needed it makes the SetActive to true and positions the prefab in the required position.
TileSpawner generates tiles as player progresses and removes tile as the player crosses them using the ObjectPooler class.
ParticeEffect stores all the particle effects and spawns when needed
