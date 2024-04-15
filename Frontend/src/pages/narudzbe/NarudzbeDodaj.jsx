import { Button, Col, Container, Form, FormSelect, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import Service from '../../services/NarudzbaService';
import { RoutesNames } from '../../constants';
import KupacService from '../../services/KupacService';
import ProizvodService from '../../services/ProizvodService';
import { useEffect, useState } from 'react';
import { dohvatiPorukeAlert } from '../../services/httpService';

export default function NarudzbeDodaj() {
  const navigate = useNavigate();

  const [kupci, setKupci] = useState([]);
  const [kupacSifra, setKupacSifra] = useState(0);

  const [proizvodi, setProizvodi] = useState([]);
  const [proizvodSifra, setProizvodSifra] = useState(0);

  async function dohvatiKupce(){
    const odgovor = await KupacService.get();
    if(!odgovor.ok){
        alert(dohvatiPorukeAlert(odgovor.podaci));
        return;
    }
    setKupci(odgovor.podaci);
    setKupacSifra(odgovor.podaci[0].sifra);
  }

  async function dohvatiProizvode(){
    const odgovor = await ProizvodService.get();
    if(!odgovor.ok){
        alert(dohvatiPorukeAlert(odgovor.podaci));
        return;
    }
    setProizvodi(odgovor.podaci);
    setProizvodSifra(odgovor.podaci[0].sifra);
  }

  async function ucitaj(){
    await dohvatiKupce();
    await dohvatiProizvode();
  }

  useEffect(()=>{
    ucitaj();
  },[]);

  async function dodaj(e) {
    //console.log(e);

    const odgovor = await Service.dodaj(e);
    if (odgovor.ok) {
      navigate(RoutesNames.NARUDZBE_PREGLED);
      return
    } 
    alert(dohvatiPorukeAlert(odgovor.podaci));
    
  }

  function handleSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);

    
    if(podaci.get('datum')=='' && podaci.get('vrijeme')!=''){
      alert('Ako postavljate vrijeme morate i datum');
      return;
    }
    let datumnarudzbe='';
    if(podaci.get('datum')!='' && podaci.get('vrijeme')==''){
      datumnarudzbe = podaci.get('datum') + 'T00:00:00.000Z';
    }else{
      datumnarudzbe = podaci.get('datum') + 'T' + podaci.get('vrijeme') + ':00.000Z';
    }



    dodaj({
     
      kupacSifra: parseInt(kupacSifra),
      proizvodSifra: parseInt(proizvodSifra),
      datumnarudzbe: datumnarudzbe,
      placanje: podaci.get('placanje'),
      ukupaniznos: podaci.get('ukupaniznos')
    });
  }

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit}>
        <Form.Group className='mb-3' controlId='kupacSifra'>
          <Form.Label>Kupac </Form.Label>
          <Form.Select
            value={kupacSifra}
            onChange={(e) => setKupacSifra(parseInt(e.target.value))}
          >
            {kupci.map((kupac, index) => (
              <option key={index} value={kupac.sifra}>
                {kupac.ime} {kupac.prezime}
              </option>
            ))}
          </Form.Select>
        </Form.Group>
        
        <Form.Group className='mb-3' controlId='proizvodSifra'>
          <Form.Label>Proizvod </Form.Label>
          <Form.Select
            value={proizvodSifra}
            onChange={(e) => setProizvodSifra(parseInt(e.target.value))}
          >
            {proizvodi && Array.isArray(proizvodi) && proizvodi.map((proizvod, index) => (
               <option key={index} value={proizvod.sifra}>
                   {proizvod.naziv} {proizvod.vrsta}
               </option>
            ))}
          </Form.Select>
        </Form.Group>

        <Form.Group className='mb-3' controlId='datumnarudzbe'>
          <Form.Label>Datum narudžbe</Form.Label>
          <Form.Control
            type='date'
            name='datumnarudzbe'
            placeholder='Datumnarudzbe'
            maxLength={50}
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='placanje'>
          <Form.Label>Plaćanje</Form.Label>
          <Form.Control
            type='text'
            name='placanje'
            placeholder='plaćanje'
            maxLength={50}
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='ukupaniznos'>
          <Form.Label>Ukupan iznos</Form.Label>
          <Form.Control
            type='text'
            name='ukupaniznos'
            placeholder='ukupaniznos'
          />
        </Form.Group>

       

        <Row>
          <Col>
            <Link className='btn btn-danger gumb' to={RoutesNames.NARUDZBE_PREGLED}>
              Odustani
            </Link>
          </Col>
          <Col>
            <Button variant='primary' className='gumb' type='submit'>
              Dodaj Narudzbu
            </Button>
          </Col>
        </Row>
      </Form>
    </Container>
  );
}