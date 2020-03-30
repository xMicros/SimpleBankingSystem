import React from "react";
import styles from "./App.module.css";
import Account from "../../containers/Account/Account";

const App = props => {
  return (
    <div className={styles.App}>
      <header>
        <h1>Simple Banking System</h1>
      </header>
      <Account />
      <footer>
        <p>Copyright &copy; 2020 Marcin Wojaczek</p>
      </footer>
    </div>
  );
};

export default App;
