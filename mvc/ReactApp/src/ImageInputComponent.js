import "./ImageInputComponent.css"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowUp, faArrowDown, faTrash  } from '@fortawesome/free-solid-svg-icons';
export function ImageInput(props){
    return (
        <div className = "InputWrapper">
            <div className="top-border">{props.title} 
            <FontAwesomeIcon icon={faArrowUp} onClick={()=>{props.changeOrder("up")}}/> 
            <FontAwesomeIcon icon={faArrowDown} onClick={() => {props.changeOrder("down")}}/> 
            <FontAwesomeIcon icon={faTrash} onClick = {props.deleteBlock}/></div>
            <select value={props.value} onChange={(event) => {props.callback(event.target[event.target.selectedIndex].innerText)}}>
                {/*props.options? props.options.map((option,idx) => {return <option value={option} key={idx}>{option}</option>})*/}
            </select>
        </div>
    )
}  