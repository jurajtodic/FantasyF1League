import React from 'react'

function SelectBy(props) {
  return (
    <div>
        <label>Select by:</label>
        <select onChange={props.function}>
            {props.list.map(item => (
                <option key={item.id} name={item.name} value={item.value}>{item.text}</option>
            ))}
        </select>
    </div>
  )
}

export default SelectBy