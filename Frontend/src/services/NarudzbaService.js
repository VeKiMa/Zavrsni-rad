import { App } from "../constants"
import { httpService,obradiGresku } from "./httpService";

const naziv = 'Narudzba';
async function get(){
   return await httpService.get('/' + naziv).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
  }

async function getBySifra(sifra) {
  return await httpService.get('/'+naziv+'/' + sifra).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
  }
  async function dodaj(entitet) {
  return await httpService.post('/' + naziv, entitet).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
  }
  async function promjeni(sifra, entitet) {
  return await httpService.put('/'+naziv+'/' + sifra, entitet).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
  }
  async function obrisi(sifra) {
    return await httpService.delete('/' + naziv + '/' + sifra).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
  }
 

export default{
    get,
    obrisi,
    dodaj,
    promjeni,
    getBySifra
};