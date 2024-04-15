import { useEffect, useState } from "react";
import {  Button, Container, Table } from "react-bootstrap";
import ProizvodService from "../../services/ProizvodService";
import { NumericFormat } from "react-number-format";
import { GrValidate } from "react-icons/gr";
import { IoIosAdd } from "react-icons/io";
import { FaEdit, FaTrash } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import { RoutesNames } from "../../constants";


export default function Proizvodi(){
    const [Proizvodi,setProizvodi] = useState();
    let navigate = useNavigate();
               
    async function dohvatiProizvode(){
        const odgovor = await ProizvodService.get();
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setProizvodi(odgovor.podaci);
    }
   
    
    async function obrisiProizvod(sifra){
        const odgovor = await ProizvodService.obrisi(sifra);
        alert(dohvatiPorukeAlert(odgovor.podaci));
        if (odgovor. ok){
            dohvatiProizvode();            
      }
    }

    useEffect(()=>{
        dohvatiProizvode();
    },[]);
    
    return(
        <Container>
            <Link to={RoutesNames.PROIZVODI_NOVI} className=" btn btn-success gumb">
               <IoIosAdd 
               size={25} 
               /> Dodaj
            </Link>  
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                         <th>Naziv</th>  
                         <th>Vrsta</th>
                         <th>Cijena</th>
                         <th>Slika</th>                       
                         <th>Akcija</th>                                             
                    </tr>
                </thead>
                <tbody>
                    {Proizvodi && Array.isArray(Proizvodi) && Proizvodi.map((proizvod,index)=>(
                       <tr key={index}>
                           <td>{proizvod.naziv}</td>
                           <td>{proizvod.vrsta}</td>                
                           <td className="desno">
                               <NumericFormat
                                value={proizvod.cijena}
                                displayType={'text'}
                                thousandSeparator= '.'
                                decimalSeparator=','
                                prefix={'€'}
                                decimalScale={2}
                                fixedDecimalScale
                               />
                           </td>
                           <td>{proizvod.slika}</td>
                           <td className="sredina">
                               <Button                                
                               variant="primary"
                               onClick={()=>{navigate(`/Proizvod/${proizvod.sifra}`)}}
                               >
                                   <FaEdit 
                                   size={25}
                               />
                               </Button> 
                                    &nbsp;&nbsp;&nbsp;
                                <Button
                                    variant="danger"
                                    onClick={()=>obrisiProizvod(proizvod.sifra)}
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