import axios from "axios";
import { ArticleForm } from "./ArticleForm";
import "./CreateArticleComponent.css";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark  } from '@fortawesome/free-solid-svg-icons';
import {ImageInput } from "./ImageInputComponent";
import { ArticleTextInput } from "./ArticleTextInputComponent";
import { useState,useEffect } from "react";
import { useParams } from "react-router";
export function CreateArticle({props}) {
const [blocks, setBlocks ] = useState([])
const [imageList, setImageList] = useState([])
const [title,setTitle] =useState("");
const [authors, setAuthors] = useState([]);
const [author, setAuthor] = useState("");
const [imageName, setImageName ] = useState("");
const [preAmble, setPreamble] = useState("");
const [pinned,setPinned] = useState(false);
const articleId = useParams().articleId;
useEffect(()=>{
    if(articleId !== undefined){
       axios.get(`https://localhost:7208/api/articles/${articleId}`).then((resp)=>{
            setTitle(resp.data.title);
            setAuthor(resp.data.author.id);
            setImageName(resp.data.imageName);
            setPreamble(resp.data.intro);
            setPinned(resp.data.pinned);
            let tmpBlocks = [...resp.data.blocks];
            setBlocks(tmpBlocks);
            
       })
    }
},[])
    
    
useEffect(()=> {
    axios.get("https://localhost:7208/api/images").then(response => {
        setImageList(response.data);
    })
},[])
useEffect(()=>{
    axios.get("https://localhost:7208/api/authors").then(response => {
        setAuthors(response.data);
    })
},[])


const changeValue = (newValue,index) =>  {
    let tmp = [...blocks];
    tmp[index] = {...tmp[index], value: newValue}
    setBlocks(tmp);
}
const appendBlock = (type,id="undef") => {
    let newKey = id==="undef"?((+new Date()) + Math.random()* 100).toString(32): id
    
    if(type === "Image"){
        setBlocks([...blocks, {type:"image",id:newKey,props:{options:imageList}}]);
    }
    else {
        setBlocks([...blocks, {type:type,id:newKey,props:{"isRequired":true}}]);
    }
}
const changeOrder = (direction, indexToChange) => {
   //Edge cases, only 1 block, or block is at position 1 and user wants to move it up or block is at ultimate position and user wants to move it down. 
   if(blocks.length === 1){
   }
   else if(indexToChange === 0 && direction === "up"){

   }
   else if(indexToChange === blocks.length-1 && direction === "down"){
   }
   else {
       let oldIndex = indexToChange;
       let newIndex = direction === 'up' ? oldIndex-1 : oldIndex+1; 
       let oldList = [...blocks];
       let firstComponent = {...oldList[oldIndex]};
       let secondComponent = {...oldList[newIndex]};
       oldList[oldIndex] = secondComponent;
       oldList[newIndex] = firstComponent;
       setBlocks(oldList);
   }
}
   const changeThings = (type,newValue) => {
        switch(type){
            case "preamble":
            setPreamble(newValue);
            break;
            case "author":
            setAuthor(newValue);
            break;
            case "pinned":
            setPinned(newValue);
            break;
            case "image":
            setImageName(newValue);
            break;
            case "title":
            setTitle(newValue);
            break;
            default:
                break;
        }
   }
   const deleteBlock = (indexToDelete) => {
       let tmpBlocks = [...blocks].filter((block,idx) => idx !== indexToDelete);
       setBlocks(tmpBlocks);
   }
    return( 
        <div className="create-article-container">
            <div className="edit-article-heading">
                <h2>{articleId === undefined ? "Skriv Artikel": "Redigera Artikel"}</h2>
                <div class="btn-container">
                    <button class="save-article-btn">Spara</button>
                    <FontAwesomeIcon icon={faXmark} 
                    onClick = { ()=> {window.location.reload(true); }} />
                </div>
            </div>
            <div className="create-article-form">
                <ArticleForm handleChange = {changeThings}
                             title= {title}
                             preamble = {preAmble}
                             pinned = {pinned}
                             imageList = {imageList}
                             imageName = {imageName}
                             authors = {authors}
                             author = {author}

                             />
            </div>
            <h2>Blocks</h2>
            {blocks.map((block,idx) => {
                let tmp = {...block.props,
                    "type": block.type,
                    "callback":(newValue)=>{changeValue(newValue,idx)},
                    "changeOrder":(direction)=>{changeOrder(direction,idx)},
                    "deleteBlock": ()=>{deleteBlock(idx)},
                    "value": block.value,
                    }                   
                if(block.type === "image"){
                    return <ImageInput key={block.id} {...tmp}/>
                }
                
                else {
                    return <ArticleTextInput key={block.id} {...tmp} />
                }})}
            <button className="create-block-button" onClick = {() => {appendBlock("paragraph")}}>Ny paragraf</button>
            <button className="create-block-button" onClick = {() => {appendBlock("quote")}}>Nytt citat</button>
            <button className="create-block-button" onClick = {() => {appendBlock("image")}}>Ny bild</button>
        </div>

    )
}