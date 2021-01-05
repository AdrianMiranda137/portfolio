import './App.css';
import {generate as id} from 'shortid';

//Components
import Header from './components/Header';
import Person from './components/Person'

function App() {
  return (
    <>
      <Header/>
      <Person
        age={23}
        birthday={new Date(1997,7,13)}
        name="Adrian"
      />
    </>
  )
}

export default App;
