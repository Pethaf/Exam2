import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import {
  BrowserRouter,
  Routes,
  Route,useNavigate } from "react-router-dom";
import App from './App';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';
import { ListArticles  } from './ListArticlesComponent';
import { CreateArticle } from "./CreateArticleComponent";
import { ListJournalists} from "./ListJournalistsComponent";
const rootElement = document.getElementById('root');

ReactDOM.render(
<BrowserRouter>
<Routes>    
    <Route path="/Admin" element = {<App />}>
      <Route path="Articles/New" element= {<CreateArticle />} />
      <Route path="Articles" element = {<ListArticles />} />
      <Route path="Articles/:articleId" element = {<CreateArticle />} />
      <Route path="Journalists" element = {<ListJournalists /> } />
      <Route path="Images" element ={<p>Images</p>} />
    </Route>
</Routes>
</BrowserRouter>,
  rootElement);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
