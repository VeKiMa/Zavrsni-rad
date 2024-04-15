import { useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Highcharts from 'highcharts';
import PieChart from 'highcharts-react-official';
import NarudzbaService from '../services/NarudzbaService';
import { dohvatiPorukeAlert } from '../services/httpService';

export default function Pocetna(){

    return(
        <>
        <Container>
            Dobro dosli na Webtrgovina aplikaciju
        </Container>        
        </>
    );

}
