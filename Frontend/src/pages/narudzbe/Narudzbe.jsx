import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import { IoIosAdd } from "react-icons/io";
import { FaEdit, FaTrash } from "react-icons/fa";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import ProgressBar from 'react-bootstrap/ProgressBar';
import moment from "moment";

import { RoutesNames } from "../../constants";
import NarudzbaService from "../../services/NarudzbaService";
import { dohvatiPorukeAlert } from "../../services/httpService";

export default function Narudzbe(){
    const [narudzbe,setNarudzbe] = useState();
    let navigate = useNavigate(); 

    async function dohvatiNarudzbe(){
        const odgovor = await NarudzbaService.get();
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setNarudzbe(odgovor.podaci);
    }
    
    async function obrisiNarudzbu() {
        const odgovor = await NarudzbaService.obrisi();
        alert(dohvatiPorukeAlert(odgovor.podaci));
        if (odgovor.ok){
            dohvatiNarudzbe();
        }
    }

      useEffect(()=>{
        dohvatiNarudzbe();
    },[]);
      
      function formatirajDatum(datumnarudzbe){
        let mdp = moment.utc(datumnarudzbe);
        if(mdp.hour()==0 && mdp.minutes()==0){
            return mdp.format('DD. MM. YYYY.');
        }
        return mdp.format('DD. MM. YYYY. HH:mm');
    }        

    return (

        <Container>
            <Link to={RoutesNames.NARUDZBE_NOVE} className="btn btn-success gumb">
                <IoIosAdd
                size={25}
                /> Dodaj
            </Link>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Kupac</th>
                        <th>Proizvod</th>
                        <th>Datum narudzbe</th>            
                        <th>Placanje</th>
                        <th>Ukupan iznos</th>
                        <th>Akcija</th>
                    </tr>
                </thead>
                <tbody>
                    {narudzbe && narudzbe.map((entitet,index)=>(
                        <tr key={index}>
                            <td>{entitet.kupacImePrezime}</td>
                            <td>{entitet.proizvodNaziv}</td>                           
                            <td>{entitet.datumnarudzbe}</td>
                            <td>{entitet.placanje}</td>
                            <td>{entitet.ukupaniznos}</td>
                            <td className="sredina">
                                    <Button
                                        variant='primary'
                                        onClick={()=>{navigate(`/narudzbe/${entitet.sifra}`)}}
                                    >
                                        <FaEdit 
                                    size={25}
                                    />
                                    </Button>
                               
                                
                                    &nbsp;&nbsp;&nbsp;
                                    <Button
                                        variant='danger'
                                        onClick={() => obrisiNarudzbu(entitet.sifra)}
                                    >
                                        <FaTrash
                                        size={25}/>
                                    </Button>

                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>

    );

}