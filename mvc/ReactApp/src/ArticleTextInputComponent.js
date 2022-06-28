import "./ArticleTextInputComponent.css";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUp, faArrowDown, faTrash  } from '@fortawesome/free-solid-svg-icons';

export function ArticleTextInput(props){
    return (
        <div className = "InputWrapper">
            <div className="top-border"> 
            <div className="name-container">{props.type}</div>
            <div className="input-btn-wrapper">
            <FontAwesomeIcon icon={faArrowUp} onClick={()=>{props.changeOrder("up")}}/> 
            <FontAwesomeIcon icon={faArrowDown} onClick={() => {props.changeOrder("down")}}/> 
            <FontAwesomeIcon icon={faTrash} onClick = {() => {props.deleteBlock()}} /></div>
            </div>
            <textarea required={props.isRequired} value={props.value} onChange= {(event) => {props.callback(event.target.value);}}/>
        </div>
    )

}