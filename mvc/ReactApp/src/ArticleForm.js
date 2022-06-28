import "./ArticleForm.css";
export function ArticleForm(props) {
/*  let authorOptions = [];
if(props.authors !== [] && props.authors !== undefined){
  authorOptions = props.authors.map((author,idx)=>{
    return <option value={author.id} key={idx}>{`${author.firstName} ${author.lastName}`}</option>})}  */
  return (
      <div className="container">
        <form>
          <label>
              Titel
              <input type ="text" required onChange={(event) => props.handleChange("title",event.target.value)} 
                     value={props.title}></input>
          </label>
          <label>
              Författare 
              <select  value = {props.author} onChange={(event) => props.handleChange("author",event.target.value)} >
                {props.authors.map((author) => {return <option 
                                                         key={author.id} 
                                                         value={author.id} >
                                                           {`${author.firstName} ${author.lastName}`} 
                                                  </option>})}
              </select>
          </label>
          <label>
              Artikelbild 
              <select value={props.imageName} onChange ={(event) => props.handleChange("image",event.target.value)}>
              {props.imageList.map((image,idx) => {return <option 
                                                         key={idx} 
                                                         value={image} 
                                                         >
                                                           {image}
                                                  </option>})}
              </select>
          </label>
          <label> Ingress
            <textarea required value={props.preamble} onChange={(event) => {props.handleChange("preamble",event.target.value)}}></textarea>
          </label>
        <label>
          Fäst artikel 
          <input type="checkbox" checked={props.pinned} onChange={(event) => {props.handleChange("pinned",event.target.checked) }}></input>
        </label>
        </form>
      </div>
    )
  }
  