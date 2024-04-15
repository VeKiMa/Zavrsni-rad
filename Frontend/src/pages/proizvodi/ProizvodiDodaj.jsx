import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RoutesNames } from "../../constants";
import ProizvodService from "../../services/ProizvodService";
import { dohvatiPorukeAlert } from '../../services/httpService';


export default function ProizvodiDodaj(){
    const navigate = useNavigate();


    async function dodajProizvod(Proizvod){
        const odgovor = await ProizvodService.dodaj(Proizvod);
        if(odgovor.ok){
          navigate(RoutesNames.PROIZVODI_PREGLED);
          return
        }
        alert(dohvatiPorukeAlert(odgovor.podaci));
    }

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
        //console.log(podaci.get('naziv'));
      
        const proizvod =
        { 
            naziv: podaci.get('naziv'),
            vrsta: podaci.get('vrsta'),
            cijena: parseFloat(podaci.get('cijena'))
        }
        dodajProizvod(); 
    }

    return (

        <Container className='mt-4'>
           
           <Form onSubmit={handleSubmit}>

                <Form.Group className='mb-3'controlId="naziv">
                    <Form.Label>Naziv</Form.Label>
                    <Form.Control 
                        type="text"
                        name="naziv"
                        placeholder='naziv proizvoda'
                        maxLength={255}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="vrsta">
                    <Form.Label>Vrsta</Form.Label>
                    <Form.Control 
                        type="text"                        
                        name="vrsta"
                        placeholder='vrsta proizvoda'
                        maxLength={255}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="cijena">
                    <Form.Label>Cijena</Form.Label>
                    <Form.Control 
                        type="number"                        
                        name="cijena Proizvoda"
                        placeholder="cijena proizvoda"
                        required
                    />
                </Form.Group>                             

                <Row className="akcije">
                    <Col>
                        <Link 
                        className="btn btn-danger gumb"
                        to={RoutesNames.PROIZVODI_PREGLED}>Odustani</Link>
                    </Col>
                    <Col>
                        <Button
                            variant="primary" className='gumb'                          
                            type="submit"
                        >
                            Dodaj proizvod
                        </Button>
                    </Col>
                </Row>
                
           </Form>

        </Container>

    );

}