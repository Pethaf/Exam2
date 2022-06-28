import "./Header.css";
import { useEffect} from "react";
import {  useNavigate,useLocation } from "react-router-dom";
export function Header(props) {
let location = useLocation();
let navigate = useNavigate();

useEffect(()=>{
    if(location.pathname.endsWith("/Admin/")){
        navigate("/Admin/Articles")
    }
        
},[])
    return (
        <header>
           <h1>Blåatoppens dagblad - Admin</h1>
        </header>
    )
}