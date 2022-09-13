import React from 'react'

function Form(props) {
  return (
    <div className="form-group row">
        <form>
            {props.list.map(form=>(
                <div key={form.id} className="col-xs-3">
                    <label htmlFor="ex2" className="form-label">{form.labelName}</label>
                    <input key={form.id} type={form.type} onChange={form.fuction} name={form.name} className="form-control" id="ex2"/>
                    {/* {type="number", onChange={fja}, name=Position} */}
                </div>
            ))}
        </form>
    </div>
  )
}

export default Form