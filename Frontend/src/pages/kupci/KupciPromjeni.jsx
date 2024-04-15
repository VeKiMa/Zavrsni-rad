import { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import KupacService from"../../services/KupacService";
import { RoutesNames } from "../../constants";
import { dohvatiPorukeAlert } from '../../services/httpService';

export default function KupciPromjeni(){
    const [kupac,setKupac] = useState({});
    
    const routeParams = useParams();
    const navigate = useNavigate();
    

    async function dohvatiKupac(){
        const odgovor = await KupacService.getBySifra(routeParams.sifra, kupac);
        if(odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setKupac(odgovor.podaci);
    }

    async function promjeniKupac(kupac){
        const odgovor = await KupacService.promjeni(routeParams.sifra,kupac);
        if(odgovor.ok){
          navigate(RoutesNames.KUPCI_PREGLED);
          return;
        }
        alert(dohvatiPorukeAlert(odgovor.podaci));
    }

    useEffect(() => {
        dohvatiKupac();
    },[]);
    
    
    function handleSubmit(e){
        e.preventDefault();

        const podaci = new FormData(e.target);
        promjeniKupac({  
            ime: podaci.get('ime'),
            prezime: podaci.get('prezime'),
            email: podaci.get('email'),
            adresa: podaci.get('adresa'),
            telefon: podaci.get('telefon')
          });
    }


    return (
        <Container className='mt-4'> 
           <Form onSubmit={handleSubmit}>

                <Form.Group className='mb-3' controlId="ime">
                    <Form.Label>Ime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={kupac.ime}
                        name="ime"
                        maxLength={255}
                        placeholder="ime"
                        required
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="prezime">
                    <Form.Label>Prezime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={kupac.prezime}
                        name="prezime"
                        maxLength={255}
                        placeholder="prezime"
                        required
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="email">
                    <Form.Label>Email</Form.Label>
                    <Form.Control 
                        type="email"
                        defaultValue={kupac.email}
                        name="email"
                        maxLength={255}
                        placeholder="email"
                        required
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="adresa">
                    <Form.Label>Adresa</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={kupac.adresa}
                        name="adresa"
                        maxLength={255}
                        placeholder="adresa"
                        required
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="telefon">
                <Form.Label>Telefon</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={kupac.telefon}
                        name="telefon"
                        maxLength={20}
                        placeholder="telefon"
     
                    />
                </Form.Group>

                <Row>
                    <Col>
                        <Link className="btn btn-danger gumb" to={RoutesNames.KUPCI_PREGLED}> 
                        Odustani
                        </Link>
                    </Col>
                    <Col>
                        <Button variant="primary" className="gumb" type="submit"> 
                            Promjeni kupca
                        </Button>
                    </Col>
                </Row>               
           </Form>

        </Container>

    );

}