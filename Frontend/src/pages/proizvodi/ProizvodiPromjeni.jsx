import { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import ProizvodService from "../../services/ProizvodService";
import { RoutesNames } from "../../constants";
import { dohvatiPorukeAlert } from "../../services/httpService";

export default function ProizvodiPromjeni(){

    const [Proizvod,setProizvod] = useState({});
    const routeParams = useParams();
    const navigate = useNavigate();

    async function dohvatiProizvod(){
        const odgovor = await ProizvodService.getBySifra(routeParams.sifra,Proizvod);
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setProizvod(odgovor.podaci);       
    }

    
    
    async function promjeniProizvod(Proizvod){
        const odgovor = await ProizvodService.promjeni(routeParams.sifra,Proizvod);
        if(odgovor.ok){
          navigate(RoutesNames.PROIZVODI_PREGLED);
        return;        
        }
        alert(dohvatiPorukeAlert(odgovor.podaci));
    }

    useEffect(()=>{   
        dohvatiProizvod();
    },[]);
    
    
    async function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
         
        promjeniProizvod ({
            naziv: podaci.get('naziv'),
            vrsta: podaci.get('vrsta'),
            cijena: parseFloat(podaci.get('cijena'))
          });
               
    }

    return (
        <Container className="mt-4"> 
           <Form onSubmit={handleSubmit}>

                <Form.Group className='mb-3' controlId="naziv">
                    <Form.Label>Naziv</Form.Label>
                    <Form.Control 
                        type="text"
                        name="naziv"
                        defaultValue={Proizvod.naziv}
                        maxLength={255}
                        placeholder="naziv"
                        required
                    />
                </Form.Group>                

                <Form.Group className='mb-3' controlId="vrsta">
                    <Form.Label>Vrsta</Form.Label>
                    <Form.Control 
                        type="text"
                        name="vrsta"
                        defaultValue={Proizvod.vrsta}
                        maxLength={255}
                        placeholder="vrsta"
                        required
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="cijena">
                    <Form.Label>Cijena</Form.Label>
                    <Form.Control 
                        type="text"
                        name="cijena"
                        defaultValue={Proizvod.cijena}
                        placeholder="cijena"
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