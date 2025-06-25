
  <h2>Summary of <code>T2LifestyleChecker</code> Project</h2>

  <h3>1. Purpose &amp; Functionality</h3>
  <p>
    A web API and React-based UI for validating NHS patients and calculating a health-risk score based on lifestyle choices.  
  </p>
  <h3>2. Architecture</h3> 
  <ul>
    <li>
      <strong>Backend</strong>: ASP.NET Core (<code>.NET 8</code>)<br />
      - <code>PatientValidationService</code> handles patient lookup and age verification.<br />
      - <code>ScoringService</code> reads scoring rules and messages from JSON config, scoring answers dynamically.
    </li>
    <li>
      <strong>Frontend</strong>: React with TypeScript<br />
      - <code>ValidateForm.tsx</code> lets users enter NHS number, surname, and date of birth.<br />
      - <code>LifestyleForm.tsx</code> fetches questions and sends answers for scoring.<br />
      - Implements typed models shared via a central <code>types/</code> folder.
    </li>
  </ul>

  <h3>3. Configurability</h3>
  <p>
    Loads <code>ScoringRules.json</code> and <code>Messages.json</code> at runtime with <code>reloadOnChange=true</code>, enabling updates to scoring logic without code changes (when using scoped <code>IOptionsSnapshot&lt;T&gt;</code>).
  </p>

  <h3>4. Clean Code &amp; SOLID Principles</h3>
  <ul>
    <li>Separation of concerns: validation, scoring, UI logic, and configuration are cleanly decoupled.</li>
    <li>Uses dependency injection (<code>HttpClientFactory</code>, <code>IConfiguration</code>, <code>ILogger</code>).</li>
    <li>Strongly-typed, immutable models with <code>init;</code> for safety.</li>
  </ul>

  <h3>5. Testing</h3>
  <ul>
    <li>Pure helper methods (<code>CalculateAge</code>) are static and easily testable.</li>
    <li>Service methods inject dependencies and support mocking and unit testing.</li>
  </ul>

<h3>API Details</h3>  

There are 5 patients configured, which should allow you to test various scenarios

<markdown-accessiblity-table data-catalyst=""><table>
<thead>
<tr>
<th>Nhs Number</th>
<th>Name</th>
<th>Age</th>
<th>DOB</th>
</tr>
</thead>
<tbody>
<tr>
<td>111222333</td>
<td>DOE, John</td>
<td>18</td>
<td>Jan 14</td>
</tr>
<tr>
<td>222333444</td>
<td>SMITH, Alice</td>
<td>25</td>
<td>Mar 2</td>
</tr>
<tr>
<td>333444555</td>
<td>CARTER, Bob</td>
<td>46</td>
<td>May 20</td>
</tr>
<tr>
<td>444555666</td>
<td>BOND, Charles</td>
<td>70</td>
<td>July 18</td>
</tr>
<tr>
<td>555666777</td>
<td>MAY, Megan</td>
<td>14</td>
<td>Nov 14</td>
</tr>
</tbody>
</table></markdown-accessiblity-table>


<h3>Project Structure</h3>
<img width="559" alt="image" src="https://github.com/user-attachments/assets/4c74e094-0b9e-4c43-bf2b-05b92e0fb8df" />


