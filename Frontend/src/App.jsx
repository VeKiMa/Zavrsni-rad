import { Route, Routes } from "react-router-dom"
import Pocetna from "./pages/Pocetna"
import { RoutesNames } from "./constants"
import NavBar from "./components/NavBar"

import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css';

import Proizvodi from "./pages/proizvodi/Proizvodi"
import ProizvodiDodaj from "./pages/proizvodi/ProizvodiDodaj"
import ProizvodiPromjeni from "./pages/proizvodi/ProizvodiPromjeni"

import Kupci from "./pages/kupci/Kupci"
import KupciDodaj from "./pages/kupci/KupciDodaj"
import KupciPromjeni from "./pages/kupci/KupciPromjeni"

import Narudzbe from "./pages/narudzbe/Narudzbe"
import NarudzbeDodaj from "./pages/narudzbe/NarudzbeDodaj"
import NarudzbePromjeni from "./pages/narudzbe/NarudzbePromjeni"

function App() {
  return (
    <>
    <NavBar />
      <Routes>
        <>
          <Route path={RoutesNames.HOME} element={<Pocetna />} />
          
                    
          <Route path={RoutesNames.PROIZVODI_PREGLED} element={<Proizvodi />} /> 
          <Route path={RoutesNames.PROIZVODI_NOVI} element={<ProizvodiDodaj />} />                              
          <Route path={RoutesNames.PROIZVODI_PROMJENI} element={<ProizvodiPromjeni />} />


          <Route path={RoutesNames.KUPCI_PREGLED} element={<Kupci />} /> 
          <Route path={RoutesNames.KUPCI_NOVI} element={<KupciDodaj />} />                              
          <Route path={RoutesNames.KUPCI_PROMJENI} element={<KupciPromjeni />} />


          <Route path={RoutesNames.NARUDZBE_PREGLED} element={<Narudzbe />} /> 
          <Route path={RoutesNames.NARUDZBE_NOVE} element={<NarudzbeDodaj />} />                              
          <Route path={RoutesNames.NARUDZBE_PROMJENI} element={<NarudzbePromjeni />} />
        </>
      </Routes>
    </>
  )
}

export default App
