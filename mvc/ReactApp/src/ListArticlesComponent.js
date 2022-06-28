import axios from "axios";
import {Spinner } from "./Spinner";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPen, faTrash  } from '@fortawesome/free-solid-svg-icons'
import "./ListArticles.css"
import { useState,useEffect, Fragment } from "react";
import { useNavigate, useLocation} from "react-router-dom";
export function ListArticles() {
    const [articles,setArticles] = useState([]);
    const [loadingState, setLoadingState] = useState(true);
    useEffect(()=>{axios.get("https://localhost:7208/api/articles").then(resp=> {
            let articles = resp.data;
            setLoadingState(false);
            setArticles(articles);
    })},[])
    let navigate = useNavigate();
    let location = useLocation();
    const DeleteArticle = (id) => {
        axios.delete(`https://localhost:7208/api/articles/${id}`);
        window.location.reload(true);
    }

    return( 
        <section className="article-list">
            {loadingState? <Spinner />: 
                <Fragment>
                <div className="article-list-wrapper">
                <h2>Artiklar</h2>
                <button className="create-article-button" onClick={()=>{navigate(`${location.pathname}/New`)}}>Skapa</button>
                </div>
                <table>
                    <tbody>
                    {articles.map((article)=>
                    {return <tr key={article.id}>
                            <td>{article.title}</td>
                            <td><FontAwesomeIcon className = "edit-article-icon" icon={faPen} onClick = {() => (navigate(`${location.pathname}/${article.id}`))}/></td>
                            <td><FontAwesomeIcon className = "delete-article-icon" icon={faTrash} onClick = {()=>(DeleteArticle(article.id)) }/></td>
                            </tr>})}
                    </tbody>
                </table>
                </Fragment>
                    
            }
        </section>
    )
}