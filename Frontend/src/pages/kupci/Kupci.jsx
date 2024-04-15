import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import KupacService from '../../services/KupacService';
import { IoIosAdd } from "react-icons/io";
import { FaEdit, FaTrash } from "react-icons/fa";
import { Link } from "react-router-dom";
import { RoutesNames } from "../../constants";
import { useNavigate } from "react-router-dom";
import { dohvatiPorukeAlert } from "../../services/httpService";

export default function Kupci(){
    const [Kupci,setKupci] = useState();
    let navigate = useNavigate(); 

    async function dohvatiKupce(){
        const odgovor = await KupacService.get();
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setKupci(odgovor.podaci);
    }

    async function obrisiKupac(sifra) {
        const odgovor = await KupacService.obrisi(sifra);       
        alert(dohvatiPorukeAlert(odgovor.podaci));
        if (odgovor.ok) {
            alert(dohvatiPorukeAlert(odgovor.podaci));
            dohvatiKupce();          
        }
      }

      useEffect(()=>{
        dohvatiKupce();
    },[]);
    
      return (

        <Container>
            <Link to={RoutesNames.KUPCI_NOVI} className="btn btn-success gumb">
                <IoIosAdd
                size={25}
                /> Dodaj
            </Link>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Ime</th>
                        <th>Prezime</th>                        
                        <th>Email</th>
                        <th>Adresa</th>
                        <th>Telefon</th>
                        <th>Akcija</th>
                    </tr>
                </thead>
                <tbody>
                {Kupci && Kupci.map((kupac,index)=>(
                       <tr key={index}>
                           <td>{kupac.ime}</td>
                           <th>{kupac.prezime}</th>                        
                           <th>{kupac.email}</th>
                           <th>{kupac.adresa}</th>                           
                           <th>{kupac.telefon}</th>                
                           <td className="sredina">                              
                               <Button                                
                                   variant="primary"
                                   onClick={()=>{navigate(`/kupci/${kupac.sifra}`)}}
                                >
                                   <FaEdit 
                                size={25}
                                />
                               </Button> 
                                    &nbsp;&nbsp;&nbsp;
                                <Button
                                    variant="danger"
                                    onClick={()=>obrisiKupac(kupac.sifra)}
                                >                                            
                                <FaTrash   
                                   size={25} 
                                   />
                                </Button> 
                           </td>
                       </tr>
                    ))}                                           
                </tbody>
            </Table>
        </Container>
    );
}