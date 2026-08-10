# Specification Preparation Agent

A structured framework to guide authors in creating complete, clear, and implementation‑ready functional or technical specifications.

---

## ## 🧭 1. Scope Definition
- [ ] Clearly define the **objective** and **problem statement**.  
- [ ] Identify the **target users**, personas, or systems involved.  
- [ ] Clarify what is **in‑scope** and **out‑of‑scope**.  
- [ ] List expected outcomes and success criteria.

---

## ## 📝 2. Requirements Gathering

### **Functional Requirements**
- [ ] Capture all business rules and workflows.  
- [ ] Document UI/UX expectations (screens, components, interactions).  
- [ ] Define API behaviors, request/response structures, and validations.  
- [ ] Include data model fields, constraints, and mapping rules.

### **Non‑Functional Requirements**
- [ ] Performance expectations (latency, concurrency, load).  
- [ ] Security (auth, authZ, encryption, PII/PHI constraints).  
- [ ] Reliability and availability requirements.  
- [ ] Logging, monitoring, and auditability.  
- [ ] Scalability and extensibility constraints.

---

## ## 🔗 3. Dependencies & Integrations
- [ ] List all internal/external service dependencies.  
- [ ] Specify integration protocols (REST, GraphQL, events, gRPC).  
- [ ] Document event structures, topics, and sequencing (if applicable).  
- [ ] Highlight upstream and downstream impacts.  
- [ ] Capture assumptions and constraints clearly.

---

## ## 🧩 4. Solution Design

### **Architecture**
- [ ] High‑level architecture diagram.  
- [ ] Sequence diagrams for key flows.  
- [ ] Component responsibilities and boundaries.  
- [ ] Data model, schemas, and state transitions.

### **API Design**
- [ ] Endpoints with naming conventions followed.  
- [ ] Request/response samples and schemas.  
- [ ] Error codes and handling strategy.  
- [ ] Pagination, filtering, and sorting rules (if applicable).

### **UI/UX Design**
- [ ] Wireframes or screen mockups.  
- [ ] Interaction rules, actions, and edge cases.  
- [ ] Accessibility considerations.

---

## ## 🔍 5. Edge Case & Error Handling Coverage
- [ ] Document expected behavior for invalid inputs and boundary conditions.  
- [ ] Specify system recovery flows and fallback logic.  
- [ ] Describe all error scenarios with standardized error codes/messages.

---

## ## 🧹 6. Clarity & Consistency
- [ ] Ensure terminology is consistent across all sections.  
- [ ] Structure the document logically with clear headings.  
- [ ] Use tables, diagrams, and examples for complex topics.  
- [ ] Align with internal documentation standards.

---

## ## 🧪 7. Test Readiness Inputs
- [ ] Define acceptance criteria for each requirement.  
- [ ] Include sample scenarios for functional and edge cases.  
- [ ] Provide data setup requirements for testing.  
- [ ] Identify metrics for validating NFRs.

---

## ## 📦 8. Final Quality Checklist
- [ ] All sections completed with no placeholders or TBDs.  
- [ ] All flows and interactions validated with stakeholders.  
- [ ] All diagrams and sample payloads validated and accurate.  
- [ ] Internal standards and conventions followed end‑to‑end.  
- [ ] Specification is understandable without a walkthrough session.

---

# ## ✅ Expected Output (Prepared Specification)

### ### **Implementation‑Ready Deliverables**
- Complete business flows  
- Functional and non‑functional requirements  
- API/UI/architecture design  
- Edge cases and error flows  
- Dependencies, constraints, assumptions  
- Acceptance criteria and test scenarios  

---

# ## 🎯 Final Goal
A **complete, unambiguous, developer‑ready specification** that minimizes rework, prevents misinterpretation, and accelerates delivery.