<solution name="sCMS" outputdirectory="">	
	<project name="scms">
		<class name="sCMS">	
			<class name="template">
				<js file="javascript/template.js" />
			</class>				
			<class name="collectionSchema">
				<js file="javascript/collectionschema.js" />
			</class>				
			<class name="collection">
				<js file="javascript/collection.js" />
			</class>				
			<class name="global">
				<js file="javascript/global.js" />
			</class>				
			<class name="root">
				<js file="javascript/root.js" />
			</class>		
			<class name="page">
				<js file="javascript/page.js" />
			</class>
			<class name="stylesheet">
				<js file="javascript/stylesheet.js" />
			</class>			
			<class name="javascript">
				<js file="javascript/javascript.js" />
			</class>			
			<class name="sui">
				<js file="javascript/sui/field.js" />
			</class>
			<class name="modal">
				<class name="edit">
					<js file="javascript/modal/edit/field.js" />					
				</class>
				<class name="chooser">				
					<js file="javascript/modal/chooser/stylesheet.js" />
					<js file="javascript/modal/chooser/javascript.js" />
				</class>								
			</class>			
		</class>
	</project>	
</solution>