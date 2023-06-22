"use strict"

class Mod {
    async postDBLoadAsync(container) {
        const db = container.resolve("DatabaseServer").getTables();
        const importerUtil = container.resolve("ImporterUtil");
        const locales = db.locales.global;
        const locations = db.locations;
        const loot = db.loot;

        const mydb = await importerUtil.loadRecursiveAsync(`${__dirname}/database/`);
        
        locations["testmap"] = mydb.locations.testmap.testmap;
        loot.staticContainers["Test map"] = mydb.locations.testmap.staticContainers;

        for (const localeKey in locales) {
            if (typeof mydb.locales[localeKey] != "undefined") {
                for (const key in mydb.locales[localeKey]) {
                    locales[localeKey][key] = mydb.locales[localeKey][key];
                }
            }
            else {
                for (const key in mydb.locales.en) {
                    locales[localeKey][key] = mydb.locales.en[key];
                }
            }
        }
    }
}

module.exports = { mod: new Mod() }