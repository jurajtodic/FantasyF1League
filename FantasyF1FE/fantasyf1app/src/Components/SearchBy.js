import React from 'react'

function SearchBy(props) {
  return (
    <div>
        <label>Search by {props.text}:</label>
        <input type={props.type} name={props.name} value={props.value} onChange={props.function}/>
    </div>
  )
}

export default SearchBy