import React from 'react'

function Button(props) {
  return (    
      <button onClick={props.function} class="btn btn-primary btn-floating mx-1">
          {props.buttonName}
      </button>    
  )
}

export default Button