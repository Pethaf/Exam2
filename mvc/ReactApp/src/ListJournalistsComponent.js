import { useEffect, useState } from "react"
import { Spinner } from "./Spinner"; 
import axios from "axios";
export function ListJournalists(props){
    const [journalists,updateJournalists] = useState([]);
    const [loadingState, setLoadingState] = useState(true);
    useEffect(()=>{axios.get("https://localhost:7208/api/authors").then(res => {
        let journos = res.data 
        updateJournalists(journos);
        setLoadingState(false);
    })},[])
    return(
        <section class="list-journalists">
            {loadingState? <Spinner />: <table><tbody>{journalists.map((journalist => {return <tr><td>{journalist.firstName}</td><td>{journalist.lastName}</td><td>{journalist.email}</td></tr>}))}</tbody></table>}
        </section>    
        )
}