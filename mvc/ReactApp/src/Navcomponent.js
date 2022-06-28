import "./Navbar.css";
import { NavLink} from "react-router-dom";

export function NavComponent(props){
    return (
        <nav>
               {props.links.map((link,idx)=>{return <NavLink to={link.to} className={ ({ isActive}) => (isActive ? "active": "")} > {link.displayName} </NavLink>})}
        </nav>
    )
}