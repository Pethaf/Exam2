import React, { Component, Fragment } from 'react';
import { Header } from "./Header"; 
import { NavComponent } from "./Navcomponent";
import './custom.css'
import { Outlet } from 'react-router';
export default class App extends Component {
    static displayName = App.name;
    render() {
        return (
            <Fragment>
                <Header />
            <div className="app-container">
            <NavComponent links = {[{displayName:"Artiklar",to:"Articles"}, 
                                   {displayName:"Journalister",to:"Journalists"}, 
                                   {displayName:"Bilder",to:"Images"}]} />
            
            
            <main>
            <Outlet></Outlet>
            </main>
            </div>
            </Fragment>
        );
  }
}
