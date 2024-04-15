import { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import ProizvodService from "../../services/ProizvodService";
import { RoutesNames } from "../../constants";
import { dohvatiPorukeAlert } from "../../services/httpService";

export default function ProizvodiPromjeni(){

    const navigate = useNavigate();
    const routeParams = useParams();
    const [proizvod,setProizvod] = useState({});

    async function dohvatiProizvod(){
        const odgovor = await ProizvodService.getBySifra(routeParams.sifra)
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setProizvod(odgovor.podaci);       
    }

    useEffect(()=>{   
        dohvatiProizvod();
    },[]);
    
    async function promjeniProizvod(proizvod){
        const odgovor = await ProizvodService.promjeni(routeParams.sifra,proizvod);
        if(odgovor.ok){
          navigate(RoutesNames.PROIZVODI_PREGLED);
        return;        
        }
        alert(dohvatiPorukeAlert(odgovor.podaci));
    }

    
    
    
    async function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
         
          const noviProizvod =
          {
            naziv: podaci.get('naziv'),
            vrsta: podaci.get('vrsta'),
            cijena: parseFloat(podaci.get('cijena')),
          };
        await promjeniProizvod(noviProizvod);       
    }

    return (
        <Container className="mt-4"> 
           <Form onSubmit={handleSubmit}>

                <Form.Group controlId="naziv">
                    <Form.Label>Naziv</Form.Label>
                    <Form.Control 
                        type="text"
                        name="naziv"
                        defaultValue={proizvod.naziv}
                        maxLength={255}
                        required
                    />
                </Form.Group>                

                <Form.Group controlId="vrsta">
                    <Form.Label>Vrsta</Form.Label>
                    <Form.Control 
                        type="text"
                        name="vrsta"
                        defaultValue={proizvod.vrsta}
                        maxLength={255}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="cijena">
                    <Form.Label>Cijena</Form.Label>
                    <Form.Control 
                        type="text"
                        name="cijena"
                        defaultValue={proizvod.cijena}
                        required
                    />
                </Form.Group>                             
                
                <Row>
                    <Col>
                        <Link 
                        className="btn btn-danger gumb" to={RoutesNames.PROIZVODI_PREGLED}>  
                        Odustani
                        </Link>
                    </Col>
                    <Col>
                        <Button variant="primary" className='gumb' type="submit">    
                            Promjeni proizvod
                        </Button>
                    </Col>
                </Row>
                
           </Form>

        </Container>

    );

}