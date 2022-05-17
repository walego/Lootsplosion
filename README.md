# Project Lootsplosion

This  MVC allows a user to create Items, Loot, Enemies, Loot Sources, and Loot Pools

## Items

Items are not a necessary table for the functionality to work, but they help the user experience.

## Loot

Loot is an item that has been stripped away of unecessary properties for program funcitionality.
A piece of Loot is automatically created upon creation of an item, using the item information.

## Enemies

Technically unecessary, but the Lootsplosion page allows a user to get a loot drop from an enemy, which may have multiple loot sources it pulls from.

## Loot Source

Loot Sources will have Loot assigned to them, and when loot is pulled from the source, it uses the rarity weights assigned to the source.
Users have the option to automatically create a loot source for a specific enemy upon enemy creation

## Loot Pools

This table is how Loot and Loot Sources are connected together. It is designed for users to be able to create loot pools from the loot source index,
or the loot source detail page. Loot pools are deleted by accessing the list of all loot pools connected to a loot source. It is possible for users
to directly interact with controller methods and views if they want, but this is not apparent without more knowledge of the program.

## Loot Pool Secret Rarity

Loot pools can be given a secret rarity, which relates to the rarity a loot source "sees" the loot pool as when pulling loot. This can allow users have more 
customization and creativity when making loot tables

## World Drops

Items and Loot have a "World Drop" property and enemies have "Pulls from world source". This is referring to a loot source that will automatically be created
for a user upon creating their first item or piece of loot. This loot source is not treated the same as other loot sources. Without going out of their way, 
the user will not be able to delete this source, and will only be able to add or remove items/loot via editing the item/loot

## Other things to note

When editing an item or piece of loot, the item type and rarity dropdown menus will automatically load the default values(common and weapon) 
**Make sure if you intend to edit an item/loot to adjust these dropdowns if necessary**

On item and enemy creation, all of the numerical stats(strength, intelligence, vitality, mobility, and crit) need a non-null value assigned to them. This can be zero
